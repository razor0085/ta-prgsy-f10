using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
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
        bool resize = true;
        PositionInfo pos;
        PositionInfo old_pos;
        PositionInfo relativeRadar;
        
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
                Thread.Sleep(80);
                if (!pos.Equals( World.getRobot(0).PositionInfo))
                {
                    int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel();
                    int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel();
                    int durchmesser = calculateGridSizeInPixel() * 2;
                    Rectangle rect = new Rectangle(xNullpunkt + (int)(pos.X * calculateGridSizeInPixel()) - durchmesser, yNullpunkt - (int)(pos.Y * calculateGridSizeInPixel()) - durchmesser, durchmesser * 2, durchmesser * 2);
                    this.Invalidate(rect);
                    //this.Invalidate();
                }
                
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

        protected override void OnResize(EventArgs e)
        {
            this.resize = true;
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            Graphics gxOff; //Offscreen graphics
            Bitmap m_bmpOffscreen = null;
            //System.Console.WriteLine(paintEvnt.GetType);

            if (m_bmpOffscreen == null) //Bitmap for doublebuffering
            {
                m_bmpOffscreen = new Bitmap(ClientSize.Width, ClientSize.Height);
            }

            gxOff = Graphics.FromImage(m_bmpOffscreen);
            gxOff.Clear(this.BackColor);
            //Draw some bitmap
            paintObstacle(gxOff);
            paintGrid(gxOff);
            paintRobots(gxOff);
            paintEvnt.Graphics.DrawImage(m_bmpOffscreen, 0, 0);

            base.OnPaint(paintEvnt);
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
                DriveInfo driveInfo = World.getRobot(i).Drive.Info;
                //System.Console.WriteLine("DistanceL : " + driveInfo.DistanceL.ToString("F3") + " DistanceR : " + driveInfo.DistanceR.ToString("F3"));
                pos = World.getRobot(i).Drive.Info.Position;
                relativeRadar = World.getRobot(i).RelativeRadarPosition;
                double freeSpace = World.getRobot(i).getFreeSpace();

                paintRobot(g, color, pos.X, pos.Y, pos.Angle * 2 * Math.PI / 360);
                paintRadarSensor(g, pos.X, pos.Y, (relativeRadar.Angle + pos.Angle) * 2 * Math.PI / 360, freeSpace);
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

        void paintRadarSensor(Graphics g, double x, double y, double angle, double freeSpace_radar)
        {
            // Koordinatenursprung in Pixel (Offset von 3 Pixel, damit der Robot auf den Koordinaten zu liegen kommt. warum?)
            int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel();
            int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel();
            double freeSpace = freeSpace_radar * calculateGridSizeInPixel();

            // Zeichnet den Radar Strahl
            Pen radar = new Pen(Color.DarkGreen);
            radar.Width = 3;
            radar.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(radar, xNullpunkt + (int)(x * calculateGridSizeInPixel()), yNullpunkt - (int)(y * calculateGridSizeInPixel()), xNullpunkt + (int)(x * calculateGridSizeInPixel()) + (int)(Math.Cos(angle) * freeSpace), yNullpunkt - (int)(y * calculateGridSizeInPixel()) - (int)(Math.Sin(angle) * freeSpace));
        }

        void paintObstacle(Graphics g)
        {
            RectangleF area = World.ObstacleMap.Area;
            int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel();
            int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel();

            // Transparente Farbe festlegen, CompactFramework unterstützt nur eine einzige Transparente Farbe
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorKey(Color.White, Color.White); // Wir wählen weiss als transparente Farbe

            //System.Console.WriteLine("Obstacle Coordinates: X=" + area.X + " Y=" + area.Y + " Width=" + area.Width + " Height=" + area.Height);

            Rectangle dstRect = new Rectangle(xNullpunkt + ((int)area.X) * calculateGridSizeInPixel(), yNullpunkt -(int)area.Y * calculateGridSizeInPixel() - (int)area.Height * calculateGridSizeInPixel(), (int)area.Width * calculateGridSizeInPixel(), (int)area.Height * calculateGridSizeInPixel());
            g.DrawImage(World.ObstacleMap.Image, dstRect, 0, 0, World.ObstacleMap.Image.Width, World.ObstacleMap.Image.Height, GraphicsUnit.Pixel, attr);
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
