using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
	public class Track
	{
		public double nominalSpeed;
		public double acceleration;
		public double coveredLength;
		public double elapsedTime;

		protected bool reverse = false;
		protected double length = 0;
		protected double pauseTime = 0;
		protected double currentSpeed = 0;
		protected PositionInfo startPosition;

		public virtual bool Done { get { return (elapsedTime > pauseTime) && (coveredLength > length); } }
		public virtual double ResidualLength { get { return length - coveredLength; } }

		public virtual void Start(PositionInfo startPosition)
		{
			this.startPosition = startPosition;
			this.coveredLength = 0;
			this.elapsedTime = 0;
		}

		public virtual void Step(double timeInterval)
		{
			elapsedTime += timeInterval;
			coveredLength += timeInterval * currentSpeed;
		}

		public virtual void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

	public class TrackPause : Track
	{
		public TrackPause(double pauseTimeSeconds)
		{
			this.pauseTime = pauseTimeSeconds;
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

	public class TrackLine : Track
	{
		public TrackLine(double length, double speed, double acceleration)
		{
			reverse = (Math.Sign(length) ^ Math.Sign(speed)) != 0;
			this.length = Math.Abs(length);
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			if (reverse)
				newSpeed = -newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

	public class TrackArcLeft : Track
	{
		public TrackArcLeft(double radius, double angle, double speed, double acceleration)
		{
			reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.angle = Math.Abs(angle);
			this.radius = Math.Abs(radius);
			this.length = Math.Abs((radius + Config.AxleLength / 2.0) * 2.0 * Math.PI * angle / 360.0);
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			if (reverse)
				newSpeed = -newSpeed;
			left.Speed = -newSpeed * (radius - Config.AxleLength / 2.0) / (radius + Config.AxleLength / 2.0);
			right.Speed = newSpeed;
		}

		public double angle;
		public double radius;
	}

	public class TrackArcRight : Track
	{
		public TrackArcRight(double radius, double angle, double speed, double acceleration)
		{
			reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.angle = Math.Abs(angle);
			this.radius = Math.Abs(radius);
			this.length = Math.Abs((radius + Config.AxleLength / 2.0) * 2.0 * Math.PI * angle / 360.0);
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			if (reverse)
				newSpeed = -newSpeed;
			left.Speed = -newSpeed;
			right.Speed = newSpeed * (radius - Config.AxleLength / 2.0) / (radius + Config.AxleLength / 2.0); ;
		}

		public double angle;
		public double radius;
	}

	public class TrackTurn : Track
	{
		public TrackTurn(double angle, double speed, double acceleration)
		{
			reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.angle = Math.Abs(angle);
			this.length = Math.Abs(Config.AxleLength / 2.0 * 2.0 * Math.PI * angle / 360.0);
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			if (reverse)
				newSpeed = -newSpeed;
			right.Speed = newSpeed;
			left.Speed = newSpeed;
		}

		public double angle;
	}

	public class TrackContourLeft : Track
	{
		public TrackContourLeft(double spacing, double speed, double acceleration)
		{
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.spacing = Math.Abs(spacing);
			this.length = double.PositiveInfinity;
		}

		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;

			const double kp = 5;
			double leftToRight;
			leftToRight = 1.0 - kp * (World.Robot.Radar.Distance - spacing);
			if (leftToRight >= 1)
			{
				left.Speed = -newSpeed;
				right.Speed = newSpeed / leftToRight;
			}
			else
			{
				left.Speed = -newSpeed * leftToRight;
				right.Speed = newSpeed;
			}
		}

		double spacing;
	}
}
