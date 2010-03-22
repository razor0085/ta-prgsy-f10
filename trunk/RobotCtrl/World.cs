using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief World definiert eine Welt in der Robot sich bewegt
     */
    public static class World
    {
        public static Robot Robot
        {
            //get { return robot.FindLast(); }
            set { robot.Add(value); }
        }

        public Robot getRobot(int index)
        {
            if (index > robot.Capacity()-1 || index < 0)
            {
                return null;
            }
            return robot[index];
        }

        public static double GetFreeSpace()
        {
            return 0;
        }

        public static ObstacleMap Obstacle
        {
            //get { return obstacle; }
            set { obstacle.Add(value); }
        }

        static List<ObstacleMap> obstacle = new List<ObstacleMap>();
        static List<Robot> robot = new List<Robot>();
        static int x;
        static int y;
    }

}
