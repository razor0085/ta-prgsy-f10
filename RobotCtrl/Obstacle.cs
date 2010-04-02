using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RobotCtrl
{
    public class Obstacle
    {
        private Point position = new Point(0, 0);
        private Size size = new Size(30, 30);
        private bool visible = true;
        private Rectangle rectangle = new Rectangle(0, 0, 30, 30);
        private Bitmap bitmap;

        public Rectangle Rectangle { get { return rectangle; } }

        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public Obstacle(Bitmap bitmap, Point position, Size size, bool visible)
        {
            this.position = position;
            this.size = size;
            this.visible = visible;
            this.rectangle = new Rectangle(position.X, position.Y, size.Width, size.Height);
        }
    }
}
