using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;

/**
 * @mainpage Hochschule f&uuml;r Technik & Architektur, HTA Luzern 
 *
 * <center><H1> PRGSY Robot </H1></center>
 * @author David Malgiaritta
 * @author Sascha Waser
 * <br><br><br>
 * 
 * 
 * Im Modul Systemnahes Programmieren an der Hochschule f&uuml;r Technik & Architektur Luzern wird
 * w&auml;hrend eines Semesters ein spezielles Projekt durchgef&uuml;hrt. Ziel des Projektes ist es,
 * anhand einer industrienahen Hard- und Software Plattform, das .NET Framework und insbesondere
 * die Programmiersprache C# kennenzulernen. <br>
 * Der Code wird auf Windows XP/Vista/7 und Visual Studio 2008 entwickelt. Der Roboter hingegen wird
 * mit Windows CE und dem .NET Compact Framework betrieben. Diese plattform unabh&auml;ngige 
 * Software-Entwicklung stellt eine besondere Herausforderung an die Studierenden.
 * 
 * <br><H2> Technische Daten </H2>
 * <H3> Antrieb </H3>
 * <pre>
 * Getriebe-&Uuml;bersetzung                     14 : 1
 * Encoder-Perioden pro Motor-Umdrehung     512 
 * Ticks pro Encoder-Periode                4 
 * Ticks pro Rad-Umdrehung                  28'672 
 * Rad-Durchmesser                          76 mm 
 * Rad-Abstand (Achsl&auml;nge)                  257 mm
 * </pre>
 * <H3> Motorsteuerung </H3>
 * <pre>
 * Motor-IC                                 LM629 (8MHz-Clock)
 * LM629 Sample Intervall                   256us
 * Leistungsstufe                           L6206
 * </pre>
 * 
 * <img src="../pictures/PrgSYS_Robot.jpg" alt="Roboter picture">
 */

namespace RobotCtrl
{
    /**
     * Klasse Robot, dient als Basis f&uuml;r einen realen oder virtuellen Roboter
     * @brief Basisklasse f&uuml;r einen Roboter
     */

    public class Robot
	{
        public event System.EventHandler Kollisionskurs;
        public event System.EventHandler EndOfObstacle;
        public event System.EventHandler switchChanged;

        /**
         * Property Console liefert eine Referenz auf die Console innerhalb des Robot Objektes
         * @see Console
         * 
         * @return Robot Console
         */ 
		public Console Console { get { return console; }}

        /**
         * Property Drive liefert eine Referenz auf Drive innerhalb des Robot Objektes
         * @see Drive
         * 
         * @return Drive f&uuml;r den Robot
         */ 
        public Drive Drive { get { return drive; } }

        /**
         * Property PositionInfo liefert oder setzt eine Referenz auf eine Postion
         */
        public PositionInfo PositionInfo
        {
            get { return drive.Position; }
            set { drive.Position = value; }
        }

        /**
         * Property RelativeRadarPosition liefert oder setzt die genaue Position und Ausrichtung des RadarSensor
         */
        public PositionInfo RelativeRadarPosition
        {
            set { relRadarPosition = value; }
            get {
                    if (relRadarPosition .X == 0)
                    {
                        //relRadarPosition = new PositionInfo(0.2, 0.1, -90);
                        relRadarPosition = new PositionInfo(0, 0, -90); // F�r Test von Obstacle Map setzen wir den RadarSensor auf den Mittelpunkt
                    }
                    return relRadarPosition; 
            }
        }

        /**
         * Property Color liefert oder setzt eine Farbe des Robot
         */
        public Color Color { get { return color; } set { color = value; } }

        /**
         * Property Radar liefert eine Referenz auf Radar innerhalb des Robot Objektes
         * @see Radar
         * 
         * @return Radar f&uuml;r den Robot
         */ 
        public Radar Radar { get { return radar; } }

        /**
         * Konstruktor f&uuml;r die Klasse Robot
         * @see #RunMode
         * 
         * @param runMode argument vom Typ enum #RunMode in Config.
         */
        public Robot(RunMode runMode)
		{
            if (!Config.IsWinCE)
            {
                this.runMode = RunMode.VIRTUAL;
            }
            else
            {
                this.runMode = RunMode.REAL;
            }
            console = new Console(runMode);
            initializeComponent();           
		}

        void initializeComponent()
        {
            running = true;
            
            drive = new Drive(runMode);
            radar = new Radar(runMode);

            measureDistance_thread = new Thread(measureDistance);
            measureDistance_thread.Priority = ThreadPriority.AboveNormal;
            measureDistance_thread.Start();

            checkSwitchState_thread = new Thread(checkSwitchState);
            checkSwitchState_thread.Start();

            // Reaktion, wenn wir kollidieren w�rden
            Kollisionskurs += KollisionsKursHandler;
        }

        private void checkSwitchState()
        {
            while (true)
            {
                Thread.Sleep(100);
                for (int i = 0; i < 4; i++)
                {
                    if (switchState[i] != console.Switches[i])
                    {
                        switchState[i] = console.Switches[i];
                        if (switchChanged != null)
                        {
                            switchChanged(this, new SwitchEvent(i));
                        }
                    }
                }
            }
        }

        /**
         * Methode pr�ft zyklisch, ob der Robot auf Kollisionskurs ist.
         * Wenn das der Fall ist, wir ein Event gez�ndet.
         */
        private void measureDistance()
        {
            double distance;
            double minimum_distance = 2.55;

            while (running)
            {
                Thread.Sleep(1);
                if (radar != null)
                {
                    distance = radar.Distance;
                    if (distance < 0.1)
                    {
                        if (Kollisionskurs != null)
                        {
                            Kollisionskurs(this, new EventArgs());
                        }
                    }

                    if (Math.Abs(distance - minimum_distance) > 0.2)
                    {
                        if (minimum_distance > distance - sensor_tolerance)
                        {
                            minimum_distance = distance;
                        }
                        else
                        {
                            if (EndOfObstacle != null)
                            {
                                EndOfObstacle(this, new EventArgs());
                                System.Console.WriteLine("EndOfObstacle");
                            }
                            minimum_distance = 2.55;
                        }
                    }
                }
            }
        }

        public void KollisionsKursHandler(Object o, EventArgs e)
        {
            System.Console.WriteLine("Collision Detect!");
            Drive.Stop();
            Kollisionskurs -= KollisionsKursHandler;
        }

        public void EndOfObstacleHandler(Object o, EventArgs e)
        {
            DriveInfo old_info = Drive.Info;
            DriveInfo actual_info = Drive.Info;
            while (old_info.DistanceL > actual_info.DistanceL - 0.3 && running)
            {
                actual_info = Drive.Info;
                Thread.Sleep(1);
            }
            Drive.Stop();
            EndOfObstacle -= this.EndOfObstacleHandler;
        }

        /**
         * Methode setzt die zu fahrende Geschwindigkeit
         */
        public void runFast(bool p)
        {
            if (p)
            {
                runline_speed = runline_speed_fast;
            }
            else
            {
                runline_speed = runline_speed_slow;
            }
        }


        /**
         * Methode veranlasst Robot, dem Hindernis entlang zu fahren, bist der Sensor
         * freie Sicht ( > 2.5m ) meldet.
         */
        public void followObstacle()
        {
            EndOfObstacle += this.EndOfObstacleHandler;
            sensor_tolerance = 1.5;
            double actual_distance = getFreeSpace();
            double old_distance = actual_distance;
            for(int i = 0; i < 3; i++)
            {
                if (running)
                {
                    //System.Console.WriteLine("1: " + Drive.Position.X + " " + Drive.Position.Y + " " + Drive.Position.Angle); 
                    drive.RunLine(10, runline_speed, 0.01);
                    drive.WaitDone();
                }
                if (running)
                {                  
                    System.Console.WriteLine("2: " + Drive.Position.X + " " + Drive.Position.Y + " " + Drive.Position.Angle); 
                    drive.RunTurn(-90, 1, 0.08);
                    drive.WaitDone();
                }
                System.Console.WriteLine("X: " + Drive.Position.X + " " + Drive.Position.Y + " " + Drive.Position.Angle); 
                EndOfObstacle += this.EndOfObstacleHandler;
            }
            EndOfObstacle -= this.EndOfObstacleHandler;
            sensor_tolerance = 0.3;
            System.Console.WriteLine("3 x Ecke umfahren!");
            if (running)
            {
                drive.RunLine(10, runline_speed, 0.01);
            }
            System.Console.WriteLine("freeSpace " + getFreeSpace());
            while (Math.Abs(getFreeSpace()) > 2 && running)
            {
                Thread.Sleep(1);
                System.Console.WriteLine("freeSpace " + getFreeSpace());
            }
            EndOfObstacle += this.EndOfObstacleHandler;
            drive.WaitDone();
            einparken();
        }

        public void einparken()
        {
            System.Console.WriteLine("Einparken! Distanz zum Parken: " + Radar.Distance);
            double luecken_tiefe = Math.Abs(getFreeSpace());
            if (running)
            {
                if (!Config.IsWinCE)
                {
                    drive.RunTurn(-90, 0.8, 0.08); // Virtueller Modus
                }
                else
                {
                    drive.RunTurn(-105, 0.8, 0.08); // Real Robot
                }
                drive.WaitDone();
            }
            if (running)
            {
                if (!Config.IsWinCE)
                {
                    drive.RunLine(luecken_tiefe - 0.2, runline_speed, 0.1);
                }
                else
                {
                    drive.RunLine(1.5 * luecken_tiefe, runline_speed, 0.1);
                }
                drive.WaitDone();
            }
        }

        /**
         * Methode gibt an, wieviel Abstand zu einem Hindernis gemessen wurde
         */
        public double getFreeSpace()
        {
            return radar.Distance;
        }

        public void Stop()
        {
            Clear();
        }

        /**
         * Methode sollte alle laufenden Threads dieses Robot Objekts beenden
         */
        public void Clear()
        {
            drive.Stop();
            drive.Reset();
            drive.Close();
            running = false;
            //checkSwitchState_thread.Join();
            measureDistance_thread.Join();
            initializeComponent();
        }

        RunMode runMode;
        Console console;
        Drive drive;
        Radar radar;
        PositionInfo relRadarPosition;
        Color color = Color.Azure;
        Thread measureDistance_thread;
        Thread checkSwitchState_thread;
        bool running ;
        bool[] switchState = { false, false, false, false };
        bool obstacleFound = false;
        double collisionDistance;
        double sensor_tolerance = 0.3;
        double runline_speed_fast = 1;
        double runline_speed_slow = 0.3;
        double runline_speed = 0.3;
    }
}
