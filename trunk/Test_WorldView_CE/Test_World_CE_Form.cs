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

        Thread fahr;
        Robot robot;

        private static Mutex MyMutex = new Mutex(false, "MyMutex");

        public Test_World_View_Form()
        {
            InitializeComponent();

            // Robot erstellen
            robot = new Robot(RunMode.REAL);
            World.Robot = robot;
            robot.Color = Color.Blue;
            robot.PositionInfo = new PositionInfo(0, 0, 90);
            robot.switchChanged += this.switchHandler;

            // WorldView erstellen
            worldView = new WorldView();
            worldView.Location = new System.Drawing.Point(370, 20);
            worldView.Size = new System.Drawing.Size(this.Size.Width - 390, this.Size.Height - 60);
            
            // ConsoleView erstellen
            consoleView = new ConsoleView();
            consoleView.Location = new System.Drawing.Point(20, 20);
            consoleView.RobotConsole = robot.Console;

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


            //robot.PositionInfo = new PositionInfo(0, 0.8, 45);

            //fahr = new Thread(runTrack);
            //fahr.Start();                      
            
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

        public void switchHandler(Object o, EventArgs e)
        {
            try
            {
                if (robot.Console.Switches[0] == true)
                {
                    // switch on
                    robot.Drive.Power = true;
                    System.Console.WriteLine("Power On!");
                    fahr = new Thread(runTrack);
                    fahr.Start();
                }
                else
                {
                    // switch off
                    robot.Drive.Power = false;
                    robot.Drive.Stop();
                    robot.Drive.WaitDone();
                    robot.Drive.WaitDone();
                    robot.Drive.Position = new PositionInfo(0, 0, 0);
                    fahr.Abort();
                    fahr.Join();

                    System.Console.WriteLine("Power Off!");
                }
            }
            catch (ThreadAbortException ex)
            {
                System.Console.WriteLine("Thread killed! " + ex);
            }
        }

        public void runTrack()
        {
            robot.Kollisionskurs += robot.KollisionsKursHandler;
            // Drehen wir uns um die eigene Achse, bis wir das Obstacle finden (max 360°)
           // robot.findObstacle();
           

            robot.followObstacle();
            //for (int i = 0; i < 4; i++)
            //{
            //    // Kontur des Obstacles entlang fahren
            //    robot.followObstacle();

            //    // Um die Ecke biegen
            //    robot.runConturRight();
            //}
        }
    }
}