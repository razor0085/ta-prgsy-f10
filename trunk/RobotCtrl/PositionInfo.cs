using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
	public struct PositionInfo
	{
		public double X;
		public double Y;
		public double Angle;

		public PositionInfo(double x, double y, double angle)
		{
			X = x;
			Y = y;
			Angle = angle;
		}
	}
}
