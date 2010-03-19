using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
    /**
     * @brief DriveCtrl_HW erbt von DriveCtrl und setzt die Befehle auf dem realen
     * Roboter auf die Hardware ab.
     */
	public class DriveCtrl_HW : DriveCtrl
	{
        /**
         * Property zum auslesen des Status
         */
		public override int Status { get { return status; } }

        /**
         * Property zum setzen des Status
         */
		public override int Command { set { command = value; } }

        /**
         * Konstruktor zum erzeugen eines DriveCtrl_HW
         * @see Config
         * 
         * @param portAddress setzt die Hardware Adresse des Ports
         */
		public DriveCtrl_HW(int portAddress)
		{
			port = portAddress;
			reset();
		}

        /**
         * Methode ruft reset() auf.
         */
		public override void Reset()
		{
			reset();
		}

        /**
         * Methode setzt Command auf einen bestimmten Wert.
         */
		void reset()
		{
			Command = 0;
			Thread.Sleep(5);
			Command = 0x80;
			Thread.Sleep(5);
			Command = 0;
			Thread.Sleep(5);
		}

		int status { get { return IOPort.Read(port); } }
		int command { set { IOPort.Write(port, value); } }

		int port;
	}
}
