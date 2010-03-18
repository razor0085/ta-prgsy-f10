using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief DigitalIn_HW, damit der Roboter Switches von der Hardware lesen kann
     */
	public class DigitalIn_HW : DigitalIn
	{

        /**
         * Property Data lesen von 4 Bit
         */
		public override int Data
		{
			get { return IOPort.Read(port); }
		}

        /**
         * Indexer Lesen eines bestimmten Bits
         * 
         * @param index int Indexnummer
         * @return bool Ist Bit gesetzt [TRUE/FALSE]
         */
		public override bool this[int bit]
		{
			get { return (IOPort.Read(port) & (1 << bit)) != 0; }
		}

        /**
         * Konstruktor der einen Port als Argument bekommt
         * @see Config
         * 
         * @param port int Port
         */
		public DigitalIn_HW(int port)
		{
			this.port = port;
		}

		int port;
	}
}
