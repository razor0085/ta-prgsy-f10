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
        WorldView worldView;
        ConsoleView consoleView;
        DriveView driveView;
        TrackView trackView;

        Thread thread;
        Robot robot;

        public Test_World_View_Form()
        {
            InitializeComponent();

            // Robot erstellen
            robot = new Robot(RunMode.REAL);
            World.Robot = robot;
            robot.Color = Color.Blue;
            robot.PositionInfo = new PositionInfo(0, 0, 0);

            // WorldView erstellen
            worldView = new WorldView();
            worldView.Location = new System.Drawing.Point(370, 20);
            
            // ConsoleView erstellen
            consoleView = new ConsoleView();
            consoleView.Location = new System.Drawing.Point(20, 20);

            // DriveView erstellen
            driveView = new DriveView(robot.Drive);
            driveView.Location = new System.Drawing.Point(20, 80);

            // TrackView erstellen
            trackView = new TrackView(robot.Drive);
            trackView.Location = new System.Drawing.Point(20, 100);

            this.Controls.Add(worldView);
            this.Controls.Add(consoleView);
            this.Controls.Add(driveView);
            this.Controls.Add(trackView);


            //robot.PositionInfo = new PositionInfo(2.5, -1, 90);

            //thread = new Thread(runTrack);
            //thread.Start();                      
            
            this.Closing += new CancelEventHandler(worldView.Form_Closing);           
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            paintComponents();
        }

        public void paintComponents()
        {
            if (worldView != null)
            {
                worldView.Size = new System.Drawing.Size(this.Size.Width - 390, this.Size.Height - 60);
            }
        }

        public void runTrack()
        {
            robot.RunLine(2, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.RunTurn(90, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.RunLine(2, 0.5, 0.1);
            robot.Drive.WaitDone();

        }
    }
}