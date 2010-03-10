using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
	public class DigitalIn_HW : DigitalIn
	{
		public override int Data
		{
			get { return IOPort.Read(port); }
		}

		public override bool this[int bit]
		{
			get { return (IOPort.Read(port) & (1 << bit)) != 0; }
		}

		public DigitalIn_HW(int port)
		{
			this.port = port;
		}

		int port;
	}
}
