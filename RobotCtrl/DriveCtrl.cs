using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
    /**
     * @brief DriveCtrl, Kommunikation mit der Hardware des Roboters
     */
	public class DriveCtrl
	{
        /**
         * Property zum Ein- und Ausschalten der Motoren
         */
		public bool Power { set { Command = (value) ? Status | 0x03 : Status & ~0x03; } }

        /**
         * Property zum Ein- und Ausschalten des rechten Motors
         */
		public bool PowerRight { get { return (Status & 0x01) != 0; } set { Command = (value) ? Status | 0x01 : Status & ~0x01; } }

        /**
         * Property zum Ein- und Ausschalten des linken Motors
         */
        public bool PowerLeft { get { return (Status & 0x02) != 0; } set { Command = (value) ? Status | 0x02 : Status & ~0x02; } }

        /**
         * Property zum auslesen des Status
         */
		public virtual int Status { get { return status; } }

        /**
         * Property zum setzen des Status
         */
		public virtual int Command { set { status = value; } } 
 
        /**
         * Standard Konstruktor von DriveCtrl
         */
		public DriveCtrl()
		{
			reset();
		}

        /**
         * Methode ruft reset() auf.
         */
		public virtual void Reset()
		{
			reset();
		}

        /**
         * Mehtode setzt den Status auf 0.
         */
		void reset()
		{
			status = 0;
		}

		int status = 0;
	}
}
