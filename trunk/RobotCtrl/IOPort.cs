using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RobotCtrl
{
	public static class IOPort
	{
		public static void Write(int port, int data)
		{
			WriteByte((ushort)port, (byte)data);
		}

		public static int Read(int port)
		{
			return ReadByte((ushort)port);
		}

		[DllImport("CEDDK.dll", EntryPoint = "WRITE_PORT_UCHAR", CharSet = CharSet.Auto)]
		private static extern void WriteByte(ushort Addr, byte Value);

		[DllImport("CEDDK.dll", EntryPoint = "READ_PORT_UCHAR", CharSet = CharSet.Auto)]
		private static extern byte ReadByte(ushort Addr);
	}
}
