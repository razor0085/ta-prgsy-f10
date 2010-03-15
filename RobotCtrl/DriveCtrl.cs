using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
	public class DriveCtrl
	{
		public bool Power { set { Command = (value) ? Status | 0x03 : Status & ~0x03; } }
		public bool PowerRight { get { return (Status & 0x01) != 0; } set { Command = (value) ? Status | 0x01 : Status & ~0x01; } }
		public bool PowerLeft { get { return (Status & 0x02) != 0; } set { Command = (value) ? Status | 0x02 : Status & ~0x02; } }

		public virtual int Status { get { return status; } }
		public virtual int Command { set { status = value; } } 
 
		public DriveCtrl()
		{
			reset();
		}

		public virtual void Reset()
		{
			reset();
		}

		void reset()
		{
			status = 0;
		}

		int status = 0;
	}
}
