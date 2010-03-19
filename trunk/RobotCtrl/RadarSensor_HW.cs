using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief RadarSensor_HW erbt von RadarSensor und interagiert mit der Hardware
     */
    public class RadarSensor_HW : RadarSensor
    {
        /**
         * Property Distance gibt die Distanz, welche vom Sensor gemessen wurde zur&uuml;ck.
         */
        public override double Distance
        {
            get
            {
                return IOPort.Read(io) / 100.0;
            }
        }

        /**
         * Konstruktor RadarSensor_HW
         * 
         * @param IOAddress Hardware Adresse des Sensors
         */
        public RadarSensor_HW(int IOAddress)
        {
            io = IOAddress;
        }

        int io;
    }
}
