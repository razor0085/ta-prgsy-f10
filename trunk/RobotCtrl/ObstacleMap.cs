using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace RobotCtrl
{
    public class ObstacleMap
    {
        const double maxLength = 2.55;
        List<Obstacle> obstList;
        bool[,] array;
        Bitmap image;
        RectangleF area;
        Obstacle obst;

        public Bitmap Image { get { return image; } set { image = value; } }
		public RectangleF Area { get { return area; } set { area = value; } }
        public Obstacle Obstacle { get { return obst; } set { obst = value; } }

		public ObstacleMap(RectangleF area, Bitmap image) {
            obstList = new List<Obstacle>();
			this.image = image;
			this.area = area;
            obst = new Obstacle(image, new Point((int)area.X, (int)area.Y), new Size((int)area.Width, (int)area.Height), true);
			this.array = new bool[image.Height, image.Width];
			for (int y = 0; y < array.GetLength(0); y++)
				for (int x = 0; x < array.GetLength(1); x++)
					array[y, x] = image.GetPixel(x, y).G < 128;
		}

		public double GetFreeSpace(PositionInfo position) {
			int x1 = xToIndex(position.X);
			int y1 = yToIndex(position.Y);
			int x2 = xToIndex(position.X + maxLength * Math.Cos(position.Angle / 180 * Math.PI));
			int y2 = yToIndex(position.Y + maxLength * Math.Sin(position.Angle / 180 * Math.PI));

			int dx = x2 - x1;
			int dy = y2 - y1;
			int absDx = Math.Abs(dx);
			int absDy = Math.Abs(dy);
			int incX = Math.Sign(dx);
			int incY = Math.Sign(dy);
			int x = x1, y = y1, err = 0;
			if (absDx >= absDy) {
				err = -absDx / 2;
				for (x = x1; x != x2; x = x + incX) {
					if ((x >= 0) && (x < array.GetLength(1)) &&
						(y >= 0) && (y < array.GetLength(0)) &&
						array[y, x])
						break;
					else {
						err += absDy;
						if (err >= 0) {
							y += incY;
							err -= absDx;
						}
					}
				}
			} else {
				err = -absDy / 2;
				for (y = y1; y != y2; y = y + incY) {
					if ((x >= 0) && (x < array.GetLength(1)) &&
						(y >= 0) && (y < array.GetLength(0)) &&
						array[y, x])
						break;
					else {
						err += absDx;
						if (err >= 0) {
							x += incX;
							err -= absDy;
						}
					}
				}
			}
			double xSpace = (x - x1) * area.Width / array.GetLength(1);
			double ySpace = (y - y1) * area.Height / array.GetLength(0);
			return Math.Sqrt(xSpace * xSpace + ySpace * ySpace);
		}

		int xToIndex(double x) {
			int index = (int)((x - area.X) * array.GetLength(1) / area.Width);
			return index;
		}

		int yToIndex(double y) {
			int index = array.GetLength(0) - (int)((y - area.Y) * array.GetLength(0) / area.Height);
			return index;
		}
	}
}
