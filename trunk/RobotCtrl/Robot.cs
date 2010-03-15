using System;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;

namespace RobotCtrl
{
	public class Robot
	{
		public Console Console { get { return console; }}

        public Drive Drive { get { return drive; } }

		public Robot(RunMode runMode)
		{
			if (!Config.IsWinCE)
				runMode = RunMode.VIRTUAL;
			console = new Console(runMode);
            drive = new Drive(this, runMode);
		}

		Console console;
        Drive drive;
	}
}
