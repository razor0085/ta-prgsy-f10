using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief Klasse Radar dient der Orientierung des Robot
     */
    public class Radar
    {
        /**
         * Property Distance gibt die Distanz zu einem Hindernis zur&uuml;ck.
         */
        public double Distance { get { return sensor.Distance; } }

        /**
         * Konstruktor f&uuml;r einen Radar
         * @see Config
         * 
         * @param runMode Der Runmode
         */
        public Radar(RunMode runMode)
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
            this.runMode = runMode;
        }

        RadarSensor sensor;
        RunMode runMode;
    }
}
