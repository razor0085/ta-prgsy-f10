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
            if (getRobot(0) != null)
            {
                PositionInfo offset = getRobot(0).RelativeRadarPosition;
                PositionInfo pos = getRobot(0).PositionInfo;
                double phi = pos.Angle / 180.0 * Math.PI;
                PositionInfo radarPos = new PositionInfo(
                    pos.X + offset.X * Math.Cos(phi) - offset.Y * Math.Sin(phi),
                    pos.Y + offset.X * Math.Sin(phi) + offset.Y * Math.Cos(phi),
                    (pos.Angle + offset.Angle) % 360);

                if (obstacleMap != null)
                {
                    return obstacleMap.GetFreeSpace(radarPos);
                }
            }
            return 2.55;
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
                        RectangleF area = new RectangleF(0, 0, 1, 1); 
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
