﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DriveTest
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[MTAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}
	}
}