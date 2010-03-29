using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RobotCtrl;

namespace RobotView
{
    public partial class WorldView : UserControl
    {
        int xMin = -1;
        int yMin = -2;

        int xMax = 5;
        int yMax = 2;

        bool running = true;
        
        public WorldView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill; // Damit das WorldView gleich gross ist wie das Form
            new Thread(new ThreadStart(this.Refresh)).Start();
        }

        public void Refresh()
        {
            while (running)
            {
                Thread.Sleep(100);
                this.Invalidate();
                System.Console.WriteLine("Robot-Position: x=" + World.getRobot(0).PositionInfo.X + " y=" + World.getRobot(0).PositionInfo.Y);
                System.Console.WriteLine("Robot-Distance: x=" + World.getRobot(0).MotorCtrl.Distance);
            }
        }

        int calculateGridSizeInPixel()
        {
            return (Width - 40) / getAbsoluteX();
        }

        int getAbsoluteX()
        {
            return xMax - xMin;
        }

        int getAbsoluteY()
        {
            return yMax - yMin;
        }

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            // Get the graphics object
            Graphics gfx = paintEvnt.Graphics;

            paintObstacle(gfx);
            paintGrid(gfx);

            //paintRobot(gfx, Color.Gold, 2, 0, 0);
            //paintRobot(gfx, Color.Green, 3.3, 1, Math.PI);
            //paintRobot(gfx, Color.Red, 0, -1, 0.5 * Math.PI);

            paintRobots(gfx);

        }

        void paintGrid(Graphics g)
        {
            // Create a new pen that we shall use for drawing the line
            Pen horizontalPen = new Pen(Color.Black);
            Pen verticalPen = new Pen(Color.Black);

            // Standard Linie
            float standardLinie = horizontalPen.Width;

            // Fette Linie
            float fetteLinie = 3;
            

            // Grösse des Forms
            int x = this.Width;
            int y = this.Height;

            // Horizontale Linien zeichnen
            for (int i = 0; i < getAbsoluteY() + 1; i++)
            {
                if (yMin + i == 0)
                {
                    horizontalPen.Width = fetteLinie;
                }
                else
                {
                    horizontalPen.Width = standardLinie; 
                }
                g.DrawLine(horizontalPen, 20, (i * calculateGridSizeInPixel()) + 20, getAbsoluteX() * calculateGridSizeInPixel() + 20, (i * calculateGridSizeInPixel()) + 20);
            }

            // Vertikale Linien zeichnen
            for (int i = 0; i < getAbsoluteX() + 1; i++)
            {
                if (xMin + i == 0)
                {
                    verticalPen.Width = fetteLinie;  
                }
                else
                {
                    verticalPen.Width = standardLinie;
                }
                g.DrawLine(verticalPen, (i * calculateGridSizeInPixel()) + 20, 20, (i * calculateGridSizeInPixel()) + 20, getAbsoluteY() * calculateGridSizeInPixel() + 20);
            }
        }


        void paintRobots(Graphics g)
        {
            for (int i = 0; i < World.countRobots(); i++)
            {
                Color color = World.getRobot(i).Color;
                double x = World.getRobot(i).PositionInfo.X;
                double y = World.getRobot(i).PositionInfo.Y;
                double angle = World.getRobot(i).PositionInfo.Angle;
                paintRobot(g, color, x, y, angle);
            }
        }

        void paintRobot(Graphics g, Color color, double x, double y, double angle)
        {
            // Koordinatenursprung in Pixel (Offset von 3 Pixel, damit der Robot auf den Koordinaten zu liegen kommt. warum?)
            int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel() + 4;
            int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel() + 4;

            // Durchmesser und Radius des Robot
            int durchmesser = calculateGridSizeInPixel() / 3;
            int radius = durchmesser / 2;

            // Zeichnet den Robot als Ellipse
            Rectangle rect = new Rectangle(xNullpunkt  + (int)(x * calculateGridSizeInPixel()), yNullpunkt - (int)(y * calculateGridSizeInPixel()), durchmesser, durchmesser);
            g.FillEllipse(new SolidBrush(color), rect);

            // Zeichnet die Fahrtrichtung im Robot (Winkel geht im Uhrzeigersinn)
            Pen fahrtrichtung = new Pen(Color.Black);
            fahrtrichtung.Width = 3;
            g.DrawLine(fahrtrichtung, xNullpunkt + (int)(x * calculateGridSizeInPixel()) + radius + 1, yNullpunkt - (int)(y * calculateGridSizeInPixel()) + radius, xNullpunkt + (int)(x * calculateGridSizeInPixel()) + radius + (int)(Math.Cos(angle) * radius) + 1, yNullpunkt - (int)(y * calculateGridSizeInPixel()) + radius + (int)(Math.Sin(angle) * radius));
        }

        void paintObstacle(Graphics g)
        {
            Image newImage = World.ObstacleMap.getImage();
            
            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(100, 100, 250, 50);
            // Create rectangle for source image.
            Rectangle srcRect = new Rectangle(0, 0, newImage.Width, newImage.Height);
            GraphicsUnit units = GraphicsUnit.Pixel;
            // Draw image to screen.
            g.DrawImage(newImage, destRect, srcRect, units);


        }
    }
}
