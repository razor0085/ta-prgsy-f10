using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
	public class DriveCtrl_HW : DriveCtrl
	{
		public override int Status { get { return status; } }
		public override int Command { set { command = value; } }


		public DriveCtrl_HW(int portAddress)
		{
			port = portAddress;
			reset();
		}

		public override void Reset()
		{
			reset();
		}

		void reset()
		{
			Command = 0;
			Thread.Sleep(5);
			Command = 0x80;
			Thread.Sleep(5);
			Command = 0;
			Thread.Sleep(5);
		}

		int status { get { return IOPort.Read(port); } }
		int command { set { IOPort.Write(port, value); } }

		int port;
	}
}
