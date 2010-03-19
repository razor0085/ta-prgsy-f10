using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief Struct, der die Informationen zu einem Drive h&auml;lt.
     */
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

        /**
         * Konstruktor f&uuml;r DriveInfo
         * 
         * @param position setzen einer PositionInfo
         * @param runtime setzen einer Laufzeit
         * @param speedL setzen der Geschwindigkeit des linken Motors
         * @param speedR setzen der Geschwindigkeit des rechten Motors
         * @param distanceL setzen der zu fahrenden Distanz des linken Motors
         * @param distanceR setzen der zu fahrenden Distanz des rechten Motors
         * @param driveStatus setzen des driveStatus
         * @param motorStatusL setzen des Motor-Status des linken Motors
         * @param motorStatusR setzen des Motor-Status des rechten Motors
         */
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
