using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RobotView;
using RobotCtrl;

namespace Test_WorldView
{
    public partial class Test_World_View_Form : Form
    {

        public Test_World_View_Form()
        {
            InitializeComponent();
            WorldView worldView = new WorldView();
            this.Controls.Add(worldView);

            Robot robot = new Robot(RunMode.VIRTUAL);
            World.Robot = robot;
            robot.Color = Color.Blue;
            robot.PositionInfo = new PositionInfo(0, 0, 0);
            //robot.PositionInfo = new PositionInfo(2.5, -1, 0);
            
            robot.RunLine(1.05, 1, 1);
            //robot.Distance = 300;
            //robot.RunArcLeft(10, 45, 1, 1);
            robot.Distance = 300;
                       
            
            this.Closing += new CancelEventHandler(worldView.Form_Closing);

        }
    }
}