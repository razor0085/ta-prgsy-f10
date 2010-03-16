using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class RadarSensor
    {
        public virtual double Distance { get { return World.GetFreeSpace(); } }
    }
}
