using System;
using System.Collections.Generic;
using System.Text;

/**
 * @file Config.cs
 */
namespace RobotCtrl
{
    /**
     * @brief Diese Enumeration gibt an, ob der Roboter virtuell auf dem Windows Rechner läuft
     * oder ob der Roboter real auf der Hardware läuft
     * 
     * @var VIRTUAL
     * @var REAL
     */
	public enum RunMode { VIRTUAL, REAL };

    /**
     * @brief Diese Klasse beinhaltet alle Adressen f&uuml;r die Ansteuerung der Roboter Hardware
     */
	public sealed class Config
	{
		// WinCE-Plattform
        /**
         * Property IsWinCE sagt aus, ob der Code auf dem Roboter (Windows CE) oder auf dem
         * normalen PC l&auml;uft
         */
		public static bool IsWinCE { get { return Environment.OSVersion.Platform == PlatformID.WinCE; } }

		// Roboter-Kennzahlen
		public const double WheelDiameter = 0.076;
		public const double AxleLength = 0.263;

		public const double TicksPerRevolution = 28672;
		public const double WheelCircumference = Math.PI * WheelDiameter;
		public const double MeterPerTick = WheelCircumference / TicksPerRevolution;

		// IO-Adressen
		public const int IOConsoleLeds = 0xF303;
		public const int IOConsoleSwitches = 0xF303;
		public const int IORadarSensor = 0xF310;
		public const int IODriveCtrl = 0xF330;
		public const int IOMotorCtrlRight = 0xF320;
		public const int IOMotorCtrlLeft = 0xF328;
	}
}
