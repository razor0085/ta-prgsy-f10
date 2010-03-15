using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
	public struct DriveInfo
	{
		public PositionInfo Position;
		public double Runtime;
		public double SpeedL;
		public double SpeedR;
		public double DistanceL;
		public double DistanceR;
		public int DriveStatus;
		public int MotorStatusL;
		public int MotorStatusR;

		public DriveInfo(PositionInfo position,
			double runtime,
			double speedL, double speedR,
			double distanceL, double distanceR,
			int driveStatus,
			int motorStatusL, int motorStatusR
			)
		{
			Position = position;
			Runtime = runtime;
			SpeedL = speedL;
			SpeedR = speedR;
			DistanceL = distanceL;
			DistanceR = distanceR;
			DriveStatus = driveStatus;
			MotorStatusL = motorStatusL;
			MotorStatusR = motorStatusR;
		}
	}
}
