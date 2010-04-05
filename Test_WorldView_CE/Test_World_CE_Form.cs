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

        private static Mutex MyMutex = new Mutex(false, "MyMutex");

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


            //robot.PositionInfo = new PositionInfo(0, 0.8, 45);

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
        }

        public void MinimumDistanceHandler(Object o, EventArgs e)
        {
            System.Console.WriteLine("MinimumDistanceHandler");
            robot.Drive.Stop();
            //robot.Drive.WaitDone();
            robot.MinimumDistance -= this.MinimumDistanceHandler;
            System.Console.WriteLine("End MinimumDistanceHandler");
        }

        public void findObstacle()
        {
            robot.MinimumDistance += this.MinimumDistanceHandler;
            robot.RunTurn(360, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
        }

        public void reduceDistance()
        {
            double freeSpace = robot.getFreeSpace() - 0.6;
            robot.RunTurn(-45, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
            robot.RunLine(freeSpace, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
            robot.RunTurn(45, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
        }

        public void followObstacle()
        {
            robot.MinimumDistance += this.MinimumDistanceHandler;
            double freeSpace = robot.getFreeSpace() - 0.3;
            robot.RunLine(2, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.RunLine(0.2, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
        }

        public void runConturRight()
        {
            double freeSpace = robot.getFreeSpace() - 0.6;
            robot.RunArcRight(0.6, 90, 0.5, 0.1);
            robot.Drive.WaitDone();
            robot.Drive.WaitDone();
        }

        public void runTrack()
        {
            // Reaktion, wenn wir kollidieren würden
            robot.Kollisionskurs += KollisionsKursHandler;
            
            // Warten bis GUI vollständig geladen ist
            Thread.Sleep(3000);
            
            // Drehen wir uns um die eigene Achse, bis wir das Obstacle finden (max 360°)
            findObstacle();

            // Reduzieren der Distanz zum Obstacle
            reduceDistance();

            for (int i = 0; i < 4; i++)
            {
                // Kontur des Obstacles entlang fahren
                followObstacle();

                // Um die Ecke biegen
                runConturRight();
            }
        }
    }
}