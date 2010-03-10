using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
	public class Console
	{
		public DigitalIn Switches { get { return switches; } }
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
