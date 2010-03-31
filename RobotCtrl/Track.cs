using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief Klasse Track, dient als Basis f&uuml;r eine Strecke.
     */
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
        protected double distance = 0;

        /**
         * Property Distance setzt die Distanz f&uuml;r den Track
         */
        public double Distance
        {
            set { distance = value; }
        }

        /**
         * Property Done gibt an, ob eine Strecke komplett abgefahren wurde
         */
		public virtual bool Done { get { return (elapsedTime > pauseTime) && (coveredLength > length); } }
        /**
         * Property ResidualLength gibt an, wie lange die Strecke ist, welche es noch abzufahren gilt.
         */
		public virtual double ResidualLength { get { return length - coveredLength; } }

        /**
         * Methode Start wird verwendet, um eine Startposition vom Typ PositionInfo anzugeben
         *
         * @param PositionInfo um den Startpunkt für den Track festzulegen
         */
		public virtual void Start(PositionInfo startPosition)
		{
			this.startPosition = startPosition;
			this.coveredLength = 0;
			this.elapsedTime = 0;
		}

        /**
         * Methode Step wird verwendet, um auszurechnen wo der Roboter stehen wird
         * nachdem er mit der aktuellen Geschwindigkeit in einem &uuml;bergebenen Zeitintervall
         * fahren würde.
         * 
         * @param Zeitintervall f&uuml;r die Berechnung
         */
		public virtual void Step(double timeInterval)
		{
			elapsedTime += timeInterval;
			coveredLength += timeInterval * currentSpeed;
		}

        /**
         * Methode SetSpeed wird verwendet, um den Motoren die neue Geschwinidkeit mitzuteilen.
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
		public virtual void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

    /**
     * @brief TrackPause wird verwendet um einen eine vorgegebene Zeit zu warten
     */
	public class TrackPause : Track
	{
        /**
         * Konstruktor TrackPause
         * 
         * @param pauseTimeSeconds Zeit in Sekunden
         */
		public TrackPause(double pauseTimeSeconds)
		{
			this.pauseTime = pauseTimeSeconds;
		}

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
        public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

    /**
     * @brief TrackLine wird verwendet um eine gerade Strecke abzufahren
     */
	public class TrackLine : Track
	{
        /**
         * Konstruktor TrackLine
         * 
         * @param length L&auml;nge der Strecke
         * @param speed Geschwindigkeit
         * @param acceleration Beschleunigung
         */
		public TrackLine(double length, double speed, double acceleration)
		{
			reverse = (Math.Sign(length) ^ Math.Sign(speed)) != 0;
			this.length = Math.Abs(length);
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
		}

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;
			if (reverse)
				newSpeed = -newSpeed;
			right.Speed = newSpeed;
			left.Speed = -newSpeed;
		}
	}

    /**
     * @brief TrackArcLeft wird verwendet um einen Bogen nach links zu fahren
     */
    public class TrackArcLeft : Track
    {
        /**
         * Konstruktor TrackArcLeft
         * 
         * @param radius Radius f&uuml;r den Bogen
         * @praram angle Winkel der abgefahren werden soll
         * @param speed Geschwindigkeit mit der der Roboter fahren soll
         * @param acceleration Beschleunigung auf dem Bogen
         */
        public TrackArcLeft(double radius, double angle, double speed, double acceleration)
        {
            reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
            this.nominalSpeed = Math.Abs(speed);
            this.acceleration = acceleration;
            this.angle = Math.Abs(angle);
            this.radius = Math.Abs(radius);
            this.length = Math.Abs((radius + Config.AxleLength / 2.0) * 2.0 * Math.PI * angle / 360.0);
        }

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
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

    /**
     * @brief TrackArcRight wird verwendet um einen Bogen nach rechts zu fahren
     */
    public class TrackArcRight : Track
    {
        /**
         * Konstruktor TrackArcRight
         * 
         * @param radius Radius f&uuml;r den Bogen
         * @praram angle Winkel der abgefahren werden soll
         * @param speed Geschwindigkeit mit der der Roboter fahren soll
         * @param acceleration Beschleunigung auf dem Bogen
         */
        public TrackArcRight(double radius, double angle, double speed, double acceleration)
        {
            reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
            this.nominalSpeed = Math.Abs(speed);
            this.acceleration = acceleration;
            this.angle = Math.Abs(angle);
            this.radius = Math.Abs(radius);
            this.length = Math.Abs((radius + Config.AxleLength / 2.0) * 2.0 * Math.PI * angle / 360.0);
        }

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
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

    /**
     * @brief TrackTurn wird verwendet um sich um die eigene Achse zu drehen
     */
	public class TrackTurn : Track
	{
        /**
         * Konstruktor TrackTurn
         * 
         * @praram angle Winkel um den gedreht werden soll
         * @param speed Geschwindigkeit mit der der Roboter sich drehen soll
         * @param acceleration Beschleunigung f&uuml;r die Drehung
         */
		public TrackTurn(double angle, double speed, double acceleration)
		{
			reverse = (Math.Sign(angle) ^ Math.Sign(speed)) != 0;
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.angle = Math.Abs(angle);
			this.length = Math.Abs(Config.AxleLength / 2.0 * 2.0 * Math.PI * angle / 360.0);
		}

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
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

    /**
     * @brief TrackContourLeft wird verwendet um eine Kontur linksherum abzufahren
     */
	public class TrackContourLeft : Track
	{
        /**
         * Konstruktor TrackContourLeft
         * 
         * @praram spacing Abstand zur Kontur
         * @param speed Geschwindigkeit mit der der Roboter fahren soll
         * @param acceleration Beschleunigung auf dem Weg um die Kontur
         */
		public TrackContourLeft(double spacing, double speed, double acceleration)
		{
			this.nominalSpeed = Math.Abs(speed);
			this.acceleration = acceleration;
			this.spacing = Math.Abs(spacing);
			this.length = double.PositiveInfinity;
		}

        /**
         * &Uuml;berschreibt die SetSpeed Methode der Basisklasse Track
         * 
         * @param newSpeed Setzt die neue Geschwindigkeit
         * @param left Referenz auf den linken Motor
         * @param right Referenz auf den rechten Motor
         */
		public override void SetSpeed(double newSpeed, MotorCtrl left, MotorCtrl right)
		{
			currentSpeed = newSpeed;

			const double kp = 5;
			double leftToRight;
            leftToRight = 1.0 - kp * (distance - spacing); // (World.Robot.Radar.Distance - spacing);
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
