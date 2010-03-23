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
        public ObstacleMap()
        {
            map = new Bitmap(@"..\..\..\hindernis.bmp");
        }

        public Bitmap getImage()
        {
            return map;
        }
    }
}
