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
        bool obstArray[];
        ObstDimension dimension;

        public ObstDimension dimension
        {
            get{return dimension;}
            set{dimension = value;}
        }

        public bool getObstacle
        { 
            get {return obstacle;}
        }
        
        public ObstacleMap(ObstDimension dimension, Bitmap map)
        {
            this.map = new Bitmap(@"..\..\..\hindernis.bmp");
            this.dimension = dimension;
            this.obstArray = new obstArray[x,y];


        }

        public Bitmap getImage()
        {
            return map;
        }
    }
}
