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
        /**
         * Property Robot zum Robots hinzuf&uuml;gen
         */
        public static Robot Robot
        {
            //get { return robot.FindLast(); }
            set { robot.Add(value); }
        }

        /**
         * Methode liefert einen Robot, sofern dieser vorhanden ist.
         * 
         * @param index nummer des gew&uuml;nschten Robot
         */
        public static Robot getRobot(int index)
        {
            if (index > robot.Capacity-1 || index < 0)
            {
                return null;
            }
            return robot[index];
        }

        /**
         * Methode Liefert den freespace
         */
        public static double GetFreeSpace()
        {
            return 0;
        }

        /**
         * Property ObstacleMap liefert oder setzt eine Hindernis-Karte
         */
        public static ObstacleMap ObstacleMap
        {
            set { obstacleMap = value; }
            get { return obstacleMap; }
        }

        static ObstacleMap obstacleMap = new ObstacleMap();
        static List<Robot> robot = new List<Robot>();
    }

}
