using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    class DigitalOut_HW : DigitalOut
    {
        public override int Data
		{
			get { return IOPort.Read(port); }
		}

		public override bool this[int bit]
		{
			get { return (IOPort.Read(port) & (1 << bit)) != 0; }
		}

		public DigitalOut_HW(int port)
		{
			this.port = port;
		}

		int port;
    }
}
