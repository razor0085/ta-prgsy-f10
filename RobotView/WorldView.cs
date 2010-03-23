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

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            // Get the graphics object
            Graphics gfx = paintEvnt.Graphics;

            paintGrid(gfx);

            paintRobot(gfx, Color.Gold, 90, 40, 30);
            paintRobot(gfx, Color.Green, 40, 40, 30);
            paintRobot(gfx, Color.Red, 140, 70, 30);
        }

        void paintGrid(Graphics g)
        {
            // Create a new pen that we shall use for drawing the line
            Pen horizontalPen = new Pen(Color.Black);
            Pen verticalPen = new Pen(Color.Red);

            // Standard Linie
            float standardLinie = horizontalPen.Width;

            // Fette Linie
            float fetteLinie = 2;

            // Grösse des Forms
            int x = this.Width;
            int y = this.Height;

            // Horizontale Linien zeichnen
            for (int i = 0; i < y - 40; i = i + 10)
            {
                if (i == 0)
                {
                    horizontalPen.Width = fetteLinie;
                }
                else
                {
                    horizontalPen.Width = standardLinie; 
                }
                g.DrawLine(horizontalPen, 20, i + 20, x - 20, i + 20);
            }

            // Vertikale Linien zeichnen
            for (int i = 0; i < x - 40; i = i + 10)
            {
                if (i == 0)
                {
                    verticalPen.Width = fetteLinie;  
                }
                else
                {
                    verticalPen.Width = standardLinie;
                }
                g.DrawLine(verticalPen, i + 20, 20, i + 20, y - 20);
            }
        }

        void paintRobot(Graphics g, Color color, int x, int y, int angle)
        {
            // Zeichnet den Robot als Ellipse
            Rectangle rect = new Rectangle(x, y, 20, 20);
            g.FillEllipse(new SolidBrush(color), rect);

            // Zeichnet die Fahrtrichtung im Robot
            Pen fahrtrichtung = new Pen(Color.Black);
            fahrtrichtung.Width = 3;
            g.DrawLine(fahrtrichtung, x + 10, y + 10, x + 20, y + 20);
        }
    }
}
