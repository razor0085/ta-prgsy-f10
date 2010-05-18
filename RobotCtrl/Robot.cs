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
        public event System.EventHandler DigitalInChanged;

		public Console Console { get { return robotConsole; } }
        public PositionInfo RelativeRadarPosition { get { return relativRadarPosition; } set { relativRadarPosition = value; } }
        public PositionInfo PositionInfo { get { return drive.Position; } set { drive.Position = value; } }
        public Color Color { get { return color; } set { color = value; } }
        public Drive Drive { get { return drive; } }

		public Robot(RunMode runMode)
		{
            if (!Config.IsWinCE)
            {
                this.runMode = RunMode.VIRTUAL;
            }
            else
            {
                this.runMode = runMode;
            }
			robotConsole = new Console(runMode);
            drive = new Drive(runMode);
            relativRadarPosition = new PositionInfo(0, 0, PositionInfo.Angle - 90);
            color = Color.Blue;
            if (this.runMode == RunMode.VIRTUAL)
            {
                radarSensor = new RadarSensor();
            }
            else
            {
                radarSensor = new RadarSensor_HW(Config.IORadarSensor);
            }
            DigitalInChanged += DigitalInChanged_Handler;
            digitalIn = new Thread(checkDigitalIn);
            digitalIn.Name = "checkDigitalIn";
            digitalIn.Start();

            
		}

        public void runLine(double distance)
        {
            drive.Power = true;
            drive.RunLine(distance, runline_speed_fast, runline_acceleration_fast);
            drive.WaitDone();
        }

        public void runArcRight(double radius, double angle)
        {
            drive.Power = true;
            drive.RunArcRight(radius, angle, runline_speed_fast, runline_acceleration_fast);
            drive.WaitDone();
        }

        public void runArcLeft(double radius, double angle)
        {
            drive.Power = true;
            drive.RunArcLeft(radius, angle, runline_speed_fast, runline_acceleration_fast);
            drive.WaitDone();
        }

        public void runTurn(double angle)
        {
            drive.Power = true;
            drive.RunTurn(angle, runline_speed_fast, runline_acceleration_fast);
            drive.WaitDone();
        }

        public double getFreeSpace()
        {
            //System.Console.WriteLine("getFreeSpace: " + Math.Abs(radarSensor.Distance));
            return Math.Abs(radarSensor.Distance);
        }

        public PositionInfo getInitialPosition()
        {
            if (runMode == RunMode.VIRTUAL)
            {
                return new PositionInfo(0, 0, 90);
            }
            else
            {
                return new PositionInfo(0, 0, 0);
            }
        }

        void checkDigitalIn()
        {
            int i = 0;
            System.Console.WriteLine("check DigitalIn");
            while (true)
            {
                for(; i < 4; i++)
                {
                    //System.Console.WriteLine("Check " + i + " " + Console.Switches[i]);
                    if (data[i] != robotConsole.Switches[i])
                    {
                        System.Console.WriteLine("Switch " + i + " changed!");
                        if (DigitalInChanged != null)
                        {
                            DigitalInChanged(robotConsole.Switches, new DigitalInEventArg(i));
                        }
                        data[i] = robotConsole.Switches[i];
                    }
                }
                i = 0;
                Thread.Sleep(10);
            }
        }

        void DigitalInChanged_Handler(Object sender, EventArgs e)
        {
            bool state = ((DigitalIn)sender)[((DigitalInEventArg)e).BitNumber];
            int switch_number = ((DigitalInEventArg)e).BitNumber;

            if (switch_number == 0 && state)
            {
                running = true;
                System.Console.WriteLine("Power On!");
                thread_followObstacle = new Thread(followObstacle);
                thread_followObstacle.Start();
            }

            if(switch_number == 0 && !state){
                running = false;
                System.Console.WriteLine("Power Off!");
                thread_followObstacle.Abort();
                drive.Stop();
            }

            if(switch_number == 3 && state){
                System.Console.WriteLine("Run Fast!");
                runline_speed = runline_speed_fast;
                runline_acceleration = runline_acceleration_fast;
                runline_distance = runline_distance_fast;
            }

            if (switch_number == 3 && !state)
            {
                System.Console.WriteLine("Run Slow!");
                runline_speed = runline_speed_slow;
                runline_acceleration = runline_acceleration_slow;
                runline_distance = runline_distance_slow;
            }
        }

        /**
         * Methode veranlasst Robot, dem Hindernis entlang zu fahren, bist der Sensor
         * freie Sicht ( > 2.5m ) meldet.
         */
        public void followObstacle()
        {
            drive.Power = true;
            fahreGeradeaus();
            dreheNachRechts();
            fahreGeradeaus();
            dreheNachRechts();
            fahreGeradeaus();
            dreheNachRechts();
            fahreBisParkingPlace();
            einparken();
        }

        private void fahreGeradeaus()
        {
            bool obstacle_found = false;
            drive.RunLine(10, runline_speed, runline_acceleration);
            while (running && !drive.Done)
            {
                //if (!Config.IsWinCE)
                
                Thread.Sleep(10);
                
                if (getFreeSpace() > 2)
                {
                    if (obstacle_found == true)
                    {
                        double distanceL = drive.Info.DistanceL;
                        while (distanceL > drive.Info.DistanceL - runline_distance)
                        {
                            
                                Thread.Sleep(10);
                            
                        }
                        drive.Stop();
                    }
                    obstacle_found = false;
                }
                else
                {
                    obstacle_found = true;
                }
                // messe Distanz zum Obstacle
            }
            drive.WaitDone();
        }

        private void dreheNachRechts()
        {
            if (running)
            {
                if (!Config.IsWinCE)
                {
                    //System.Console.WriteLine("2: " + Drive.Position.X + " " + Drive.Position.Y + " " + Drive.Position.Angle); 
                    drive.RunTurn(-90, 1, 0.08);
                }
                else
                {
                    drive.RunTurn(-95, 1, 0.08);
                }
            }
            drive.WaitDone();
        }

        private void fahreBisParkingPlace()
        {
            bool obstacle_found = false;
            drive.RunLine(10, runline_speed, runline_acceleration);
            while (running && !drive.Done)
            {
                //if (!Config.IsWinCE)
                {
                    Thread.Sleep(10);
                }
                if (getFreeSpace() > 0.8)
                {
                    if (obstacle_found == true)
                    {
                        double distanceL = drive.Info.DistanceL;
                        while (distanceL > drive.Info.DistanceL - runline_distance)
                        {
                            //if (!Config.IsWinCE)
                            {
                                Thread.Sleep(10);
                            }
                        }
                        drive.Stop();
                    }
                    obstacle_found = false;
                }
                else
                {
                    obstacle_found = true;
                }
                // messe Distanz zum Obstacle
            }
            drive.WaitDone();
        }

        void einparken()
        {
            System.Console.WriteLine("Einparken! Distanz zum Parken: " + radarSensor.Distance);
            double distanz_zur_parkluecke = radarSensor.Distance;

            dreheNachRechts();

            if (running)
            {
                if (!Config.IsWinCE)
                {
                    drive.RunLine(distanz_zur_parkluecke - 0.2, runline_speed, 0.8); // Virtual Modus
                }
                else
                {
                    drive.RunLine(distanz_zur_parkluecke - 0.1, runline_speed, 0.1); // Real Modus
                }
                drive.WaitDone();
            }
        }

        bool[] data = { false, false, false, false };
        RunMode runMode;
        bool running = true;
		Console robotConsole;
        Drive drive;
        RadarSensor radarSensor;
        PositionInfo relativRadarPosition;
        Color color;
        Thread digitalIn;
        Thread thread_followObstacle;

        double runline_speed_fast = 1;
        double runline_speed_slow = 0.3;
        double runline_speed = 0.3;

        double runline_acceleration_fast = 0.1;
        double runline_acceleration_slow = 0.005;
        double runline_acceleration = 0.005;

        double runline_distance_fast = 0.3;
        double runline_distance_slow = 0.4;
        double runline_distance = 0.4;

	}

    public class DigitalInEventArg : EventArgs
    {
        public int BitNumber
        {
            get { return bitNumber; }
        }

        int bitNumber;

        public DigitalInEventArg(int bitNumber)
        {
            this.bitNumber = bitNumber;
        }
    }

}
