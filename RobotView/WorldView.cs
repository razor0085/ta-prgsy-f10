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
        int yMin = -4;

        int xMax = 5;
        int yMax = 4;

        bool running = true;
        
        public WorldView()
        {
            InitializeComponent();
            //this.Dock = DockStyle.Fill; // Damit das WorldView gleich gross ist wie das Form
            new Thread(new ThreadStart(this.Refresh)).Start();   
        }

        public void Refresh()
        {
            while (running)
            {
                Thread.Sleep(10);
                this.Invalidate();
                //System.Console.WriteLine("Robot-Position: x=" + World.getRobot(0).PositionInfo.X + " y=" + World.getRobot(0).PositionInfo.Y);
            }
        }

        int calculateGridSizeInPixel()
        {
            return Width / getAbsoluteX();
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
                g.DrawLine(horizontalPen, 0, i * calculateGridSizeInPixel(), getAbsoluteX() * calculateGridSizeInPixel(), i * calculateGridSizeInPixel());
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
                g.DrawLine(verticalPen, i * calculateGridSizeInPixel(), 0, i * calculateGridSizeInPixel(), getAbsoluteY() * calculateGridSizeInPixel());
            }
        }


        void paintRobots(Graphics g)
        {
            for (int i = 0; i < World.countRobots(); i++)
            {
                Color color = World.getRobot(i).Color;
                PositionInfo pos = World.getRobot(i).getPosition();
                paintRobot(g, color, pos.X, pos.Y, Math.Abs(pos.Angle) * 2 * Math.PI / 360);
            }
        }

        void paintRobot(Graphics g, Color color, double x, double y, double angle)
        {
            // Koordinatenursprung in Pixel (Offset von 3 Pixel, damit der Robot auf den Koordinaten zu liegen kommt. warum?)
            int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel();
            int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel();

            // Durchmesser und Radius des Robot
            int durchmesser = calculateGridSizeInPixel() / 3;
            int radius = durchmesser / 2;

            // Zeichnet den Robot als Ellipse
            Rectangle rect = new Rectangle(xNullpunkt  + (int)(x * calculateGridSizeInPixel()) - radius, yNullpunkt - (int)(y * calculateGridSizeInPixel()) - radius, durchmesser, durchmesser);
            g.FillEllipse(new SolidBrush(color), rect);

            // Zeichnet die Fahrtrichtung im Robot (Winkel geht im Uhrzeigersinn)
            Pen fahrtrichtung = new Pen(Color.Red);
            fahrtrichtung.Width = 3;
            g.DrawLine(fahrtrichtung, xNullpunkt + (int)(x * calculateGridSizeInPixel()), yNullpunkt - (int)(y * calculateGridSizeInPixel()), xNullpunkt + (int)(x * calculateGridSizeInPixel()) + (int)(Math.Cos(angle) * radius), yNullpunkt - (int)(y * calculateGridSizeInPixel()) - (int)(Math.Sin(angle) * radius));
        }

        void paintObstacle(Graphics g)
        {
            Image newImage = World.ObstacleMap.getImage();
            
            // Create rectangle for displaying image.
            Rectangle destRect = new Rectangle(100, 100, 250, 50);
            // Create rectangle for source image.
            //Rectangle srcRect = new Rectangle(0, 0, newImage.Width, newImage.Height);
            GraphicsUnit units = GraphicsUnit.Pixel;
            // Draw image to screen.
            //g.DrawImage(newImage, destRect, srcRect, units);


        }

        public void Form_Closing(object sender, CancelEventArgs cArgs)
        {
            running = false;
            Thread.Sleep(1000);
            World.clear();
            //MessageBox.Show("Form Closing Event....");
        }
    }
}
