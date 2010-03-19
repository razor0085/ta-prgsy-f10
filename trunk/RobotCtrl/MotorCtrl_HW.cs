using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
    /**
     * @brief MotorCtrl_HW erbt von MotorCtrl und setzt die Kommandos auf die Robot Hardware ab.
     */
	public class MotorCtrl_HW : MotorCtrl
	{
		// Treiber-SW zum Motor-Controller-IC LM629

		// !! Achtung !!
		// !! ======= !!
		// Falls der Zugriff auf diese Klasse nicht übergeordnet synchronisiert wird,
		// müssen alle "lock(hwLock)" entkommentiert werden!

        /**
         * Property Status gibt den Status zur&uuml;ck.
         */
		public override int Status { get { return readStatus(); } }

        /**
         * Property Ready gibt an ob der Robot bereit ist.
         */
		public override bool Ready { get { return (readStatus() & 0x01) == 0; } }

        /**
         * Property Acceleration setzt die Beschleunigung a.
         */
		public override double Acceleration { set { setAcceleration(value); } }

        /**
         * Property Speed liest oder schreibt eine Geschwindigkeit.
         */
		public override double Speed { set { setSpeed(value); } get { return nominalSpeed; } }

        /**
         * Property Ticks gibt die abgelaufenen Ticks zur&uuml;ck.
         */
		protected override int Ticks { get { return getTicks(); } }

        /**
         * Konstruktor MotorCtrl_HW
         * 
         * @param IOAddress die Hardware Adresse f&uuml;r den Motor
         */
		public MotorCtrl_HW(int IOAddress)
		{
			io = IOAddress;
			reset();
		}

        /**
         * Methode Close ruft stop auf.
         */
		public override void Close()
		{
			stop();
		}

        /**
         * Methode Reset ruft reset auf.
         */
		public override void Reset(){
			reset();
		}

        /**
         * Methode setzt die Hardware in gang.
         */
		public override void Go()
		{
			lock (hwLock)
			{
				writeCmd(0x01);
			}
		}

        /**
         * Methode Stop stoppt die Ausf&uuml;hrung.
         */
		public override void Stop()
		{
			lock (hwLock)
			{
				stop();
			}
		}

        /**
         * Methode SetPID schreibt irgendwelche Daten auf die Hardware.
         */
		public override void SetPID(int proportional, int integral, int derivative, int integralLimit, int derivativeInterval)
		{
			lock (hwLock)
			{
				writeCmd(0x1E);
				writeShort(((derivativeInterval & 0xFF) << 8) | 0x0F);
				writeShort(proportional);
				writeShort(integral);
				writeShort(derivative);
				writeShort(integralLimit);
				writeCmd(0x04);
			}
		}

		// ========
		// private
		// ========

		protected const double samplePeriod = 256E-6;
		protected const double speedScale = samplePeriod / Config.MeterPerTick * (1 << 16);
		protected const double accelerationScale = samplePeriod * samplePeriod / Config.MeterPerTick * (1 << 16);

		int io;
		object hwLock = new object();

		void reset()
		{
			lock (hwLock)
			{
				Thread.Sleep(20);
				IOPort.Write(io, 0x00);
				Thread.Sleep(20);
				IOPort.Write(io, 0x02);			// Define Home
				Thread.Sleep(50);
			//	SetPID(100, 20, 1000, 1000, 1);
				SetPID(1500, 200, 8000, 1000, 1);
			}
		}

		void stop()
		{
			writeCmd(0x1F);
			writeShort(0x0100);
			writeCmd(0x01);
		}

		int getTicks()
		{
			lock (hwLock)
			{
				writeCmd(0x0A);
				return readInt();
			}
		}

		void setSpeed(double speed)
		{
			lock (hwLock)
			{
				nominalSpeed = speed;
				int velocity = (int)Math.Abs(speedScale * speed);
				int mode = (speed >= 0) ? 0x1808 : 0x0808;
				writeCmd(0x1F);
				writeShort(mode);
				writeInt(velocity);
			}
		}

		void setAcceleration(double acceleration)
		{
			lock (hwLock)
			{
				int accel = (int)(accelerationScale * acceleration);
				writeCmd(0x1F);
				writeShort(0x0820);
				writeInt(accel);
			}
		}

		void waitReady()
		{
			waitReady(200);
		}

		void waitReady(int timeout)
		{
			const int sleepTime = 1;
			int time = 0;
			while (!Ready)
			{
				Thread.Sleep(sleepTime);
				time += sleepTime;
				if (time > timeout)
					throw new ApplicationException("waitReady-Timeout: Motor Controller IC LM629.");
			}
		}

		void writeCmd(byte cmd)
		{
			waitReady();  
			IOPort.Write(io, cmd);
		}

		void writeShort(int val)
		{
			waitReady();
			IOPort.Write(io + 1, (val >> 8) & 0xFF);
			IOPort.Write(io + 1, val & 0xFF);
		}

		void writeInt(int val)
		{
			writeShort(((val >> 16) & 0xFFFF));
			writeShort(((val >> 0) & 0xFFFF));
		}

		int readStatus()
		{
			return IOPort.Read(io);
		}

		int readShort()
		{
			waitReady();
			int val = IOPort.Read(io + 1);
			val = (val << 8) | IOPort.Read(io + 1);
			return val;
		}

		int readInt()
		{
			int val = readShort();
			val = (val << 16) | readShort();
			return val;
		}
	}
}
