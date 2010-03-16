using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class RadarSensor_HW : RadarSensor
    {
        public override double Distance
        {
            get
            {
                return IOPort.Read(io) / 100.0;
            }
        }

        public RadarSensor_HW(int IOAddress)
        {
            io = IOAddress;
        }

        int io;
    }
}
