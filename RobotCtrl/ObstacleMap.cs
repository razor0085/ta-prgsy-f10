using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RobotCtrl
{
    public class ObstacleMap
    {
        Bitmap map;
        bool[,] obstArray;
        RectangleF area;

        public RectangleF dimension
        {
            get{return area;}
            set{area = value;}
        }

       // public bool getObstacle
        //{ 
        //    get {return obstacle;}
        //}
        
        public ObstacleMap(RectangleF area, Bitmap map)
        {
            this.map = new Bitmap(@"..\..\..\hindernis.bmp");
            this.dimension = area;
            this.obstArray = new bool[map.Height,map.Width];


        }

        public Bitmap getImage()
        {
            return map;
        }
    }
}
