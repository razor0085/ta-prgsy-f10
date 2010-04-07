using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace RobotCtrl
{
    /**
     * @brief ObstacleMap, verwaltet alle Hindernisse f&uuml;r den virtuellen Modus
     */
    public class ObstacleMap
    {
        const double maxRadarDiscoveryLength = 2.55;
        List<Obstacle> obstacleList; // Erweiterung geplant mit mehreren Hindernissen
        bool[,] array;
        Bitmap image;
        RectangleF area;

        /**
         * Property zum Lesen des Obstacle Images
         */
        public Bitmap Image { get { return image; } }

        /**
         * Property zum Lesen der Obstacle Dimension
         */
        public RectangleF Area { get { return area; } } //set { area = value; } }


        /**
         * Konstruktor für ObstacleMap
         * 
         */
		public ObstacleMap(RectangleF area, Bitmap image) {
            obstacleList = new List<Obstacle>();
            this.image = image;
            
            System.Console.WriteLine("image width: " + image.Width + " height: " + image.Height); 
			this.area = area;
			this.array = new bool[this.image.Height, this.image.Width];
			for (int y = 0; y < array.GetLength(0); y++)
				for (int x = 0; x < array.GetLength(1); x++)
					array[y, x] = this.image.GetPixel(x, image.Height - 1 - y).G < 128;
		}


        /**
         * Methode berechnet den Abstand zum Hindernis
         * 
         * @param PositionInfo Positionsinformationen mit Fusspunkt und Winkel des RadarSensor
         * @return double Abstand zum Obstacle
         */
		public double GetFreeSpace(PositionInfo position) {
            //System.Console.WriteLine("Position: x=" + position.X + " y=" + position.Y + " angle=" + position.Angle);

            // Maximale Sensor Länge
            int maxRadarLength = (int)(maxRadarDiscoveryLength * array.GetLength(1) / area.Width);

            // RadarSensor Fusspunkt
            int indexOfX = getIndexOfX(position.X); // Fusspunkt X des RadarSensors
            int indexOfY = getIndexOfY(position.Y); // Fusspunkt Y des RadarSensors

            // RadarSensor maximal möglicher Endpunkt
            int radarEndpointX = getIndexOfX(position.X + maxRadarDiscoveryLength * Math.Cos(position.Angle / 180 * Math.PI));
            int radarEndpointY = getIndexOfY(position.Y + maxRadarDiscoveryLength * Math.Sin(position.Angle / 180 * Math.PI));

            // X und Y Abstand zwischen Sensor Fusspunkt und Sensor Endpunkt
            int differenzX = radarEndpointX - indexOfX;
            int differenzY = radarEndpointY - indexOfY;

            // X und Y Absolut Abstand zwischen Sensor Fusspunkt und Sensor Endpunkt
            int absDifferenzX = Math.Abs(differenzX);
            int absDifferenzY = Math.Abs(differenzY);

            // Inkrementalwerte für Schleifen bestimmen (-1, 0, 1)
            // differenz < 0 -> -1
            // differenz = 0 -> 0
            // differenz > 0 -> 1
            int incX = Math.Sign(differenzX);
            int incY = Math.Sign(differenzY);
            int x = indexOfX, y = indexOfY, err = 0;

            // Drehrichtung des Sensor evaluieren (horizontal oder vertikal)
            if (absDifferenzX >= absDifferenzY)
            {
                berechneAbstandHorizontal(radarEndpointX, absDifferenzX, absDifferenzY, incX, incY, ref x, ref y, ref err);
            }
            else
            {
                berechneAbstandVertikal(radarEndpointY, absDifferenzX, absDifferenzY, incX, incY, ref x, ref y, ref err);
            }
            double xSpace = (x - indexOfX) * area.Width / array.GetLength(1);
            double ySpace = (y - indexOfY) * area.Height / array.GetLength(0);

            return Math.Sqrt(xSpace * xSpace + ySpace * ySpace);
		}

        /**
         * Methode berechnet den Abstand zum Hindernis, wenn der Sensor Vertikal steht
         */
        void berechneAbstandVertikal(int radarEndpointY, int absDifferenzX, int absDifferenzY, int incX, int incY, ref int x, ref int y, ref int err)
        {
            err = -absDifferenzY / 2;
            for (; y != radarEndpointY; y += incY)
            {
                if ((x >= 0) && (x < array.GetLength(1)) &&
                    (y >= 0) && (y < array.GetLength(0)) &&
                    array[y, x])
                    break;
                else
                {
                    err += absDifferenzX;
                    if (err >= 0)
                    {
                        x += incX;
                        err -= absDifferenzY;
                    }
                }
            }
        }

        /**
         * Methode berechnet den Abstand zum Hindernis, wenn der Sensor Horizontal steht
         */
        void berechneAbstandHorizontal(int radarEndpointX, int absDifferenzX, int absDifferenzY, int incX, int incY, ref int x, ref int y, ref int err)
        {
            err = -absDifferenzX / 2;
            for (; x != radarEndpointX; x += incX)
            {
                if ((x >= 0) && (x < array.GetLength(1)) &&
                    (y >= 0) && (y < array.GetLength(0)) &&
                    array[y, x])
                    break;
                else
                {
                    err += absDifferenzY;
                    if (err >= 0)
                    {
                        y += incY;
                        err -= absDifferenzX;
                    }
                }
            }
        }

        /**
         * Mit dieser Methode finden wir heraus, ob X links, rechts oder auf gleicher Höhe wie
         * das Obstacle platziert ist
         */
        int getIndexOfX(double x)
        {
            return (int)((x - area.X) * array.GetLength(1) / area.Width);
        }

        /**
         * Mit dieser Methode finden wir heraus, ob Y oben, unten oder auf gleicher Höhe wie
         * das Obstacle platziert ist
         */
        int getIndexOfY(double y)
        {
            return (int)((y - area.Y) * array.GetLength(0) / area.Height);
        }
	}
}
