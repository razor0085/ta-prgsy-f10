using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;
using RobotView;

namespace Test_CE
{
    public partial class Test_CE_Form : Form
    {
        public Test_CE_Form()
        {
            InitializeComponent();

            Robot robot = new Robot(RunMode.REAL);
            ConsoleView consoleView = new ConsoleView();
            consoleView.Location = new System.Drawing.Point(25, 55);
            consoleView.RobotConsole = robot.Console;
            this.Controls.Add(consoleView);
        }
    }
}