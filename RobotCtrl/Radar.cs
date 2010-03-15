using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class Radar
    {
        public float Distance { get { return distance; } }

        public Radar(Robot robot, RunMode runMode)
        {
            this.robot = robot;
            this.runMode = runMode;
        }

        Robot robot;
        RunMode runMode;
        float distance;
    }
}
