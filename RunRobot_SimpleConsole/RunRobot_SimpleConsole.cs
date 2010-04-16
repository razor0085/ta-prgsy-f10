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

        static void Main(string[] args)
        {
            RunRobot_SimpleConsole runRobot_SimpleConsole = new RunRobot_SimpleConsole();
            System.Console.ReadLine();
        }

        public RunRobot_SimpleConsole()
        {
            robot = new Robot(RunMode.REAL);
            robot.PositionInfo = new PositionInfo(0, 0, 0);
        }
    }
}
