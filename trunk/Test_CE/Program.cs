using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using RobotCtrl;

namespace Test_CE
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //RobotCtrl.Console console = new RobotCtrl.Console(RobotCtrl.RunMode.REAL);
            Application.Run(new Test_CE_Form());
        }
    }
}