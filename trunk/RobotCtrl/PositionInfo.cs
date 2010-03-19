using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief Sturct PositionInfo enth&auml;lt Koordinaten
     */
	public struct PositionInfo
	{
		public double X;
		public double Y;
		public double Angle;

        /**
         * Konstruktor PositionInfo
         * 
         * @param x ist die X-Koordinate
         * @param y ist die Y-Koordinate
         * @param angle ist der Winkel
         */
		public PositionInfo(double x, double y, double angle)
		{
			X = x;
			Y = y;
			Angle = angle;
		}
	}
}
