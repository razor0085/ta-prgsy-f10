using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RobotCtrl;
using System.Threading;
using RobotView;

namespace RunRobot_SimpleConsole
{
    class RunRobot_SimpleConsole
    {
        Robot robot;
        DigitalIn_HW di;
        Thread thread;
        Thread fahr;
        bool state = false;

        bool fahre_umHindernis = false;

        public event System.EventHandler switchChanged;

        static void Main(string[] args)
        {
            RunRobot_SimpleConsole runRobot_SimpleConsole = new RunRobot_SimpleConsole();
            System.Console.ReadLine();            
        }

        public RunRobot_SimpleConsole()
        {
            switchChanged += new EventHandler(switchHandler);
            robot = new Robot(RunMode.REAL);
            di = new DigitalIn_HW(Config.IOConsoleSwitches);
            robot.PositionInfo = new PositionInfo(0, 0, 0);
            thread = new Thread(checkSwitchState);
            fahre_umHindernis = true;
            thread.Start();
        }

        public void fahren()
        {
            if (fahre_umHindernis)
            {
                robot.Kollisionskurs += robot.KollisionsKursHandler;
                fahreUmHindernis();
            }
            else
            {
                robot.Drive.RunLine(1, 0.2, 0.1);
                //robot.Distance = 30;
                while (robot.Drive.Done != true && state == true)
                {
                    Thread.Sleep(10);
                }
                robot.Drive.RunArcLeft(1, 90, 0.2, 0.1);
                //robot.Distance = 30;
                while (robot.Drive.Done != true && state == true)
                {
                    Thread.Sleep(10);
                }
                robot.Drive.RunArcRight(1, 90, 0.2, 0.1);
                //robot.Distance = 30;
                while (robot.Drive.Done != true && state == true)
                {
                    Thread.Sleep(10);
                }
                robot.Drive.RunTurn(180, 0.2, 0.1);
                //robot.Distance = 30;
            }
            
        }

        public void checkSwitchState()
        {
            
            while (true)
            {
                if (state != di[0])
                {
                    state = di[0];
                    if (switchChanged != null)
                    {
                        switchChanged(this, null);
                    }
                }
                Thread.Sleep(10);
            }
        }

        public void switchHandler(Object o, EventArgs e)
        {
            try
            {
                if (di[0] == true)
                {
                    // switch on
                    robot.Drive.Power = true;
                    robot.Run();
                    System.Console.WriteLine("Power On!");
                    fahr = new Thread(fahren);
                    fahr.Start();
                }
                else
                {
                    // switch off
                    robot.Drive.Power = false;
                    robot.Drive.Stop();
                    robot.Drive.Position = new PositionInfo();
                    fahr.Abort();
                    fahr.Join();

                    System.Console.WriteLine("Power Off!");
                }
                if (di[3] == true)
                {
                    robot.Clear();
                }
            }
            catch (ThreadAbortException ex)
            {
                System.Console.WriteLine("Thread killed! " + ex);
            }
        }

        void fahreUmHindernis()
        {
            // Drehen wir uns um die eigene Achse, bis wir das Obstacle finden (max 360°)
           // robot.findObstacle();

            // Reduzieren der Distanz zum Obstacle
            
            robot.followObstacle();
        }
    }
}
