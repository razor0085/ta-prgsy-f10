using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
            robot.Color = Color.Blue;
            robot.PositionInfo = new PositionInfo(2.5, -1, 0);
            //Drive drive = robot.Drive;
            //drive.Power = true;
            //drive.RunLine(10, 1, 1);
            //drive.RunArcLeft(10, 90, 1, 1);

            robot.Go();

            //robot.Drive.Power = true;
            //robot.Go();
            //robot.Drive.RunLine(3, 10, 1);
            
            //robot.Drive.RunArcRight(70, 90, 3, 1);
            //robot.Drive.Stop();
            World.Robot = robot;

        }
    }
}