using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public static class World
    {
        public static Robot Robot
        {
            get { return robot; }
            set { robot = value; }
        }

        public static double GetFreeSpace()
        {
            return 0;
        }

        static Robot robot;
    }

}
