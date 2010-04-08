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
        Thread fahr;
        bool state = false;
        bool[] switchState = { false, false, false, false };

        bool fahre_umHindernis = false;

        static void Main(string[] args)
        {
            RunRobot_SimpleConsole runRobot_SimpleConsole = new RunRobot_SimpleConsole();
            System.Console.ReadLine();            
        }

        public RunRobot_SimpleConsole()
        {
            robot = new Robot(RunMode.REAL);
            robot.switchChanged += new EventHandler(switchHandler);
            di = new DigitalIn_HW(Config.IOConsoleSwitches);
            robot.PositionInfo = new PositionInfo(0, 0, 0);
            fahre_umHindernis = true;
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

        public void switchHandler(Object o, EventArgs e)
        {
            try
            {
                if (robot.Console.Switches[0] != switchState[0]){
                    switchState[0] = robot.Console.Switches[0];
                    if(robot.Console.Switches[0] == true)
                    {
                         // switch on
                         robot.Drive.Power = true;
                         System.Console.WriteLine("Power On!");
                         fahr = new Thread(fahreUmHindernis);
                         fahr.Start();
                    }
                    else
                    {
                        // switch off
                        robot.Drive.Power = false;
                        robot.Stop();
                        fahr.Abort();
                        fahr.Join();
                        
                        System.Console.WriteLine("Power Off!");
                    }
                }

                if (robot.Console.Switches[1] == true)
                {
                    // schnell fahren!
                    System.Console.WriteLine("schnell fahren!");
                    robot.runFast(true);
                }
                else
                {
                    // langsam fahren!
                    System.Console.WriteLine("langsam fahren!");
                    robot.runFast(false);
                }

                
            }
            catch (ThreadAbortException ex)
            {
                System.Console.WriteLine("Thread killed! " + ex);
            }
        }

        void fahreUmHindernis()
        {
            robot.Kollisionskurs += robot.KollisionsKursHandler;
            robot.followObstacle();
        }
    }
}
