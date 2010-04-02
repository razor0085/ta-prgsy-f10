using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

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
            if (index > robot.Count - 1 || index < 0)
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
            get 
            {
                    if (obstacleMap == null)
                    {
                        RectangleF area = new RectangleF(1, 3, 2, 1); 
                        obstacleMap = new ObstacleMap(area, new Bitmap(@"..\..\..\hindernis.bmp"));
                    }
                    return obstacleMap; 
            }
        }

        /**
         * Methode gibt an, wieviele Robot Instanzen vorhanden sind
         */
        public static int countRobots(){
            return robot.Count;
        }

        public static void clear()
        {
            for (int i = 0; i < robot.Count; i++){
                getRobot(i).Clear();
            }
            robot.Clear();
        }

        static ObstacleMap obstacleMap; // = new ObstacleMap();
        static List<Robot> robot = new List<Robot>();
    }

}
