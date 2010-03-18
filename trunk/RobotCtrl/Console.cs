using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief Console f&uuml;r den Roboter 
     */
	public class Console
	{
        /**
         * Property Switches Gibt ein Schalter Objekt zur&uuml;ck
         * @see DigitalIn
         * 
         * @return switches Objekt vom Typ DigitalIn
         */
		public DigitalIn Switches { get { return switches; } }

        /**
         * Property Lamps gibt ein Lampen Objekt zur&uuml;ck
         * @see DigitalOut
         * 
         * @return lamps Objekt vom Typ DigitalOut
         */
		public DigitalOut Lamps { get { return lamps; } }

		public Console(RunMode mode)
		{
			if (!Config.IsWinCE)
				mode = RunMode.VIRTUAL;

			if (mode == RunMode.VIRTUAL)
			{
				switches = new DigitalIn();
				lamps = new DigitalOut();
			}
			else
			{
				switches = new DigitalIn_HW(Config.IOConsoleSwitches);
				lamps = new DigitalOut_HW(Config.IOConsoleLeds);
			}
		}

		DigitalIn switches;
		DigitalOut lamps;
	}
}
