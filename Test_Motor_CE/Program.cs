using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MotorTest
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main()
		{
			Application.Run(new Test_Motor_CE_Form());
		}
	}
}