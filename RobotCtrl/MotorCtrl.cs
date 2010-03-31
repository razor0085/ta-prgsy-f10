using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
    /**
     * @brief MotorCtrl spricht die Motoren des Robot an.
     */
	public class MotorCtrl
	{
        /**
         * Property Status liefert den Status.
         */
		public virtual int Status { get { return status; } }

        /**
         * Property Ready gibt true zur&uuml;ck.
         */
		public virtual bool Ready { get { return true; } }

        /**
         * Property Distance zum setzen oder lesen einer Distanz.
         */
		public virtual double Distance { get { return getDistance(); } set { distance = value; } }

        /**
         * Property Speed setzt oder liest die Reisegeschwindigkeit.
         */
		public virtual double Speed { set { setSpeed(value); } get { return nominalSpeed; } }

        /**
         * Property Acceleration setzt eine Beschleunigung a.
         */
		public virtual double Acceleration { set { setAcceleration(value); } }

        /**
         * Property Stopped liest aus der Hardware, ob der Robot gestoppt wurde.
         */
		public bool Stopped { get { return (Status & 0x80) == 0; } }

        /**
         * Property Ticks gibt die anzahl Ticks des Schrittmotors zur&uuml;ck.
         */
		protected virtual int Ticks { get { return ticks; } }
		
        /**
         * Konstruktor MotorCtrl.
         */
		public MotorCtrl()
		{
			reset();
			ticks = 0;
			run = true;
			if (!(this is MotorCtrl_HW))
			{
				thread = new Thread(simulation);
				thread.Priority = ThreadPriority.AboveNormal;
				thread.Start();
			}
		}

        /**
         * Methode Reset ruft reset auf.
         */
		public virtual void Reset()
		{
			reset();
		}

        /**
         * Methode Go schreibt den Go Status.
         */
		public virtual void Go()
		{
			status = 0x80;
		}

        /**
         * Methode Stop schreibt den Stop status.
         */
		public virtual void Stop()
		{
			status = 0x00;
		}

        /**
         * Methode Close stoppt den laufenden Thread und wartet auf dessen Beendigung.
         */
		public virtual void Close()
		{
			run = false;
			if (thread != null)
				thread.Join();
		}

        /**
         * Methode SetPID tut noch nichts!
         */
		public virtual void SetPID(int proportional, int integral, int derivative, int integralLimit, int derivativeInterval)
		{
		}

        /**
         * Methode reset schreibt die Default Werte in die Variablen.
         */
		void reset()
		{
			nominalSpeed = 0;
			currentSpeed = 0;
			oldTicks = ticks;
			acceleration = 0.25;
			SetPID(100, 20, 1000, 1000, 1);
		}

        /**
         * Methode setSpeed setzt die Reisegeschwindigkeit.
         * 
         * @param speed setzt die Geschwindigkeit in m/s.
         */
		void setSpeed(double speed)
		{
			nominalSpeed = speed;
		}

        /**
         * Methode setAcceleration setzt die Beschleunigiung a.
         * 
         * @param accel setzt die Beschleunigung in m/s^2.
         */
		void setAcceleration(double accel)
		{
			acceleration = accel;
		}

        /**
         * Methode getDistance berechnet die gefahrene Distanz.
         * 
         * @return gibt die gefahrene Distanz in m zr&uuml;ck.
         */
		double getDistance()
		{
            int currentTicks = Ticks;
            distance += (currentTicks - oldTicks) * Config.MeterPerTick;
            oldTicks = currentTicks;
			return distance;
		}

        /**
         * Methode simulation f&uuml;hrt die Simulation aus.
         */
		void simulation()
		{
			int time = Environment.TickCount;
			while (run)
			{
				if (!Stopped)
				{
					int dtime = Environment.TickCount - time;
					time += dtime;
					double dt = dtime / 1000.0;
					ticks += (int)(dt * currentSpeed / Config.MeterPerTick);
					if (nominalSpeed >= currentSpeed)
						currentSpeed = Math.Min(nominalSpeed, currentSpeed + dt * acceleration);
					else
						currentSpeed = Math.Max(nominalSpeed, currentSpeed - dt * acceleration);
				}
				Thread.Sleep(1);
			}
		}

		protected double nominalSpeed;		// m/s
		double distance;					// m
		double currentSpeed;				// m/s
		double acceleration;				// m/s^2
		int oldTicks;
		int ticks;
		bool run;
		Thread thread;
		int status;
	}
}
