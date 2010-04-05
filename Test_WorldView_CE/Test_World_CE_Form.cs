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

        double vorgabe = 1;

        Thread thread;
        Robot robot;

        bool stop = false;

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
            worldView.Size = new System.Drawing.Size(this.Size.Width - 390, this.Size.Height - 60);
            
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


            robot.PositionInfo = new PositionInfo(0.5, 1.5, 90);

            thread = new Thread(runTrack);
            thread.Start();                      
            
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

        public void KollisionsKursHandler(Object o, EventArgs e)
        {
            if (stop == false)
            {
                stop = true;
                robot.Drive.Stop();
                robot.Drive.WaitDone();
                robot.Drive.RunTurn(45, 0.1, 0.1);
                robot.Drive.WaitDone();
                stop = false;
                
            }
            //robot.Drive.Run();
            //System.Console.WriteLine("Kollison detect!");
        }

        public void runTrack()
        {
            robot.Kollisionskurs += KollisionsKursHandler;
            double freeSpace = robot.getFreeSpace();
            double minimal_freeSpace = 2.55;

            while (freeSpace >= 2.50)
            {
                robot.RunTurn(10, 0.5, 0.5);
                robot.Drive.WaitDone();
                freeSpace = robot.getFreeSpace();
                if (freeSpace < minimal_freeSpace)
                    minimal_freeSpace = freeSpace;
            }

            while (true)
            {
                freeSpace = robot.getFreeSpace();
                robot.RunLine(0.5, 0.5, 0.1);
                robot.Drive.WaitDone();
                freeSpace = robot.getFreeSpace();
                if (freeSpace < minimal_freeSpace)
                {
                    System.Console.WriteLine("richtige Richtung!");
                }
                else
                {
                    System.Console.WriteLine("falsche Richtung!");
                }
                // freeSpace > vorgabe, dann nach rechts fahren
                if (freeSpace > (vorgabe - 0.2))
                {
                    robot.RunArcRight(1, 10, 1, 0.1);
                    robot.Drive.WaitDone();
                }
                    // freeSpace < vorgabe, dann nach links fahren
                else if (freeSpace < (vorgabe + 0.2))
                {
                    robot.RunArcLeft(1, 10, 1, 0.1);
                    robot.Drive.WaitDone();
                }
                else
                {
                    robot.RunLine(0.5, 1, 0.1);
                    robot.Drive.WaitDone();
                }
            }

        }
    }
}