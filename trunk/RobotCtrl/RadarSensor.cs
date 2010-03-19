using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief RadarSensor ist ein Sensor
     */
    public class RadarSensor
    {
        /**
         * Property um die Distanz zu einem Hindernis in Erfahrung zu bringen
         */
        public virtual double Distance { get { return World.GetFreeSpace(); } }
    }
}
