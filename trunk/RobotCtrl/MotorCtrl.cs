using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
	public class MotorCtrl
	{
		public virtual int Status { get { return status; } }
		public virtual bool Ready { get { return true; } }
		public virtual double Distance { get { return getDistance(); } set { distance = value; } }
		public virtual double Speed { set { setSpeed(value); } get { return nominalSpeed; } }
		public virtual double Acceleration { set { setAcceleration(value); } }
		public bool Stopped { get { return (Status & 0x80) == 0; } }

		protected virtual int Ticks { get { return ticks; } }
		
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

		public virtual void Reset()
		{
			reset();
		}

		public virtual void Go()
		{
			status = 0x80;
		}

		public virtual void Stop()
		{
			status = 0x00;
		}

		public virtual void Close()
		{
			run = false;
			if (thread != null)
				thread.Join();
		}

		public virtual void SetPID(int proportional, int integral, int derivative, int integralLimit, int derivativeInterval)
		{
		}

		void reset()
		{
			nominalSpeed = 0;
			currentSpeed = 0;
			oldTicks = ticks;
			acceleration = 0.25;
			SetPID(100, 20, 1000, 1000, 1);
		}

		void setSpeed(double speed)
		{
			nominalSpeed = speed;
		}

		void setAcceleration(double accel)
		{
			acceleration = accel;
		}

		double getDistance()
		{

			//???????????????????????????????????
			//???????????????????????????????????
			throw new ApplicationException("Ihre Ergänzung in Drive.getDistance fehlt.");
			//???????????????????????????????????
			//???????????????????????????????????

			return distance;
		}

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
