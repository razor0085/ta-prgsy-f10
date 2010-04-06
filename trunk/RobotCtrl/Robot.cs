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
        public event System.EventHandler MinimumDistance;

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
                        relRadarPosition = new PositionInfo(0.2, 0.1, -45);
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
                runMode = RunMode.VIRTUAL;
            }
            else
            {
                runMode = RunMode.REAL;
            }
            console = new Console(runMode);
            drive = new Drive(runMode);
            radar = new Radar(runMode);

            thread = new Thread(measureDistance);
            thread.Start();
            //drive.Distance = radar.Distance;

            // Reaktion, wenn wir kollidieren würden
            Kollisionskurs += KollisionsKursHandler;
            
            running = true;
		}

        /**
         * Methode prüft zyklisch, ob der Robot auf Kollisionskurs ist.
         * Wenn das der Fall ist, wir ein Event gezündet.
         */
        private void measureDistance()
        {
            double distance;
            double minimum_distance = 2.55;

            while (running)
            {
                Thread.Sleep(5);
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

                    if (Math.Abs(distance - minimum_distance) > 0.1)
                    {
                        if (distance < minimum_distance)
                        {
                            minimum_distance = distance;
                        }
                        else
                        {
                            if (MinimumDistance != null)
                            {
                                MinimumDistance(this, new EventArgs());
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
        }

        public void MinimumDistanceHandler(Object o, EventArgs e)
        {
            Drive.Stop();
            MinimumDistance -= this.MinimumDistanceHandler;
        }

        public void findObstacle()
        {
            MinimumDistance += this.MinimumDistanceHandler;
            Drive.RunTurn(360, 0.1, 0.1);
            Drive.WaitDone();
            Drive.WaitDone();
        }

        public void reduceDistanceToObstacle()
        {
            // Berechne Strecke bis zum Hindernis, minus 0.4 m
            double freeSpace = getFreeSpace() - 0.4;
            // Drehe den Robot, damit er senkrecht zum Hindernis steht
            Drive.RunTurn(-45, 0.2, 0.1);
            Drive.WaitDone();
            Drive.WaitDone();
            // Reduziere Distanz zum Hindernis
            Drive.RunLine(freeSpace, 0.2, 0.1);
            Drive.WaitDone();
            Drive.WaitDone();
             // Drehe zurück in damit der Robot wieder den Ursprungswinkel zum Hindernis hat
            Drive.RunTurn(45, 0.2, 0.1);
            Drive.WaitDone();
            Drive.WaitDone();
        }

        /**
         * Methode veranlasst Robot, dem Hindernis entlang zu fahren, bist der Sensor
         * freie Sicht ( > 2.5m ) meldet.
         */
        public void followObstacle()
        {
            MinimumDistance += this.MinimumDistanceHandler;           
            double actual_distance = getFreeSpace();
            double old_distance = actual_distance;
            while (getFreeSpace() < 2.5 && running)
            {
                Thread.Sleep(1000);
                actual_distance = getFreeSpace();
                System.Console.WriteLine("Aktuelle Distanz: " + actual_distance);
                
                // Solange der Abstand zum Hindernis gleich bleibt, fahren
                if (old_distance == actual_distance)
                {
                    Drive.RunLine(2, 0.5, 0.1);
                    Drive.WaitDone();
                }

                // Robot hat sich vom Hindernis entfernt, Korrektur berechnen und fahren
                if (old_distance < actual_distance)
                {

                }

                // Robot ist auf Hindernis zu gefahren, Kollision könnte eintreten
                // Korrektur berechnen und fahren
                if (old_distance > actual_distance)
                {

                }

            }
        }

        public void runConturRight()
        {
            Drive.RunArcRight(0.6, 90, 0.5, 0.1);
            Drive.WaitDone();
        }

        /**
         * Methode gibt an, wieviel Abstand zu einem Hindernis gemessen wurde
         */
        public double getFreeSpace()
        {
            return radar.Distance;
        }

        /**
         * Methode sollte alle laufenden Threads dieses Robot Objekts beenden
         */
        public void Clear()
        {
            drive.Stop();
            drive.Close();
            running = false;
        }

        Console console;
        Drive drive;
        Radar radar;
        PositionInfo relRadarPosition;
        Color color = Color.Azure;
        Thread thread;
        bool running;
	}
}
