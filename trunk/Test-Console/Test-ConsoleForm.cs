using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RobotView;

namespace Test_Console
{
    public partial class Form1 : Form
    {
        ConsoleView[] consoleView = new ConsoleView[4];
        RobotCtrl.Console console = new RobotCtrl.Console(RobotCtrl.RunMode.VIRTUAL);

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 4; i++)
            {
                consoleView[i] = new ConsoleView();
                consoleView[i].RobotConsole = console;
                consoleView[i].Location = new System.Drawing.Point(5, i * 50 + 5);
                this.Controls.Add(consoleView[i]);
            }
            console.Lamps.Data = 0x05;
            console.Switches.Data = 0x03;
        }
    }
}
