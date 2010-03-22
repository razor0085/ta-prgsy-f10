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
        public WorldView()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            // Get the graphics object
            Graphics gfx = paintEvnt.Graphics;
            // Create a new pen that we shall use for drawing the line
            Pen myPen = new Pen(Color.Black);
            Pen verticalPen = new Pen(Color.Red);
            // Loop and create a new line 10 pixels below the last one
            for (int i = 20; i < 280; i = i + 10)
            {
                gfx.DrawLine(myPen, 20, i, 270, i);
                gfx.DrawLine(verticalPen, i, 20, i, 270);
            }
        }
    }
}
