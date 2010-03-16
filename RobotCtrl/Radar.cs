using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class Radar
    {
        public double Distance { get { return distance; } }

        public Radar(Robot robot, RunMode runMode)
        {
            if (!Config.IsWinCE)
            {
                runMode = RunMode.VIRTUAL;
            }
            if (runMode == RunMode.VIRTUAL)
            {
                sensor = new RadarSensor();
            }
            else
            {
                sensor = new RadarSensor_HW(Config.IORadarSensor);
            }
            this.robot = robot;
            this.runMode = runMode;
        }

        RadarSensor sensor;
        Robot robot;
        RunMode runMode;
        double distance;
    }
}
