using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class World
    {
        public static Robot Robot
        {
            get { return robot; }
        }

        public World(Robot robot, RunMode runMode)
        {
            //this.robot = robot;
            this.runMode = runMode;
        }

        static Robot robot;
        RunMode runMode;
    }

}
