using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RobotView
{
    public partial class WorldView : UserControl
    {
        int xMin = -1;
        int yMin = -2;

        int xMax = 5;
        int yMax = 2;

        public WorldView()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill; // Damit das WorldView gleich gross ist wie das Form
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

            paintGrid(gfx);

            paintRobot(gfx, Color.Gold, 2, 0, 0);
            paintRobot(gfx, Color.Green, 2, 1, Math.PI);
            paintRobot(gfx, Color.Red, 0, -1, 0.5 * Math.PI);
        }

        void paintGrid(Graphics g)
        {
            // Create a new pen that we shall use for drawing the line
            Pen horizontalPen = new Pen(Color.Black);
            Pen verticalPen = new Pen(Color.Black);

            // Standard Linie
            float standardLinie = horizontalPen.Width;

            // Fette Linie
            float fetteLinie = 2;

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

        void paintRobot(Graphics g, Color color, double x, double y, double angle)
        {
            // Koordinatenursprung in Pixel
            int xNullpunkt = Math.Abs(xMin) * calculateGridSizeInPixel();
            int yNullpunkt = Math.Abs(yMax) * calculateGridSizeInPixel();

            // Durchmesser und Radius des Robot
            int durchmesser = calculateGridSizeInPixel() / 3;
            int radius = durchmesser / 2;

            // Zeichnet den Robot als Ellipse
            Rectangle rect = new Rectangle(xNullpunkt + (int)(x * calculateGridSizeInPixel()), yNullpunkt + (int)(y * calculateGridSizeInPixel()), durchmesser, durchmesser);
            g.FillEllipse(new SolidBrush(color), rect);

            // Zeichnet die Fahrtrichtung im Robot (Winkel geht im Uhrzeigersinn)
            Pen fahrtrichtung = new Pen(Color.Black);
            fahrtrichtung.Width = 3;
            g.DrawLine(fahrtrichtung, xNullpunkt + (int)(x * calculateGridSizeInPixel()) + radius, yNullpunkt + (int)(y * calculateGridSizeInPixel()) + radius, xNullpunkt + (int)(x * calculateGridSizeInPixel()) + radius + (int)(Math.Cos(angle) * radius), yNullpunkt + (int)(y * calculateGridSizeInPixel()) + radius + (int)(Math.Sin(angle) * radius));
        }
    }
}
