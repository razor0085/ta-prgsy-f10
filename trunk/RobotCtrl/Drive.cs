using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
    /**
     * @brief Drive, damit der Roboter herumfahren kann
     */
	public class Drive
	{
        /**
         * Property Position Abrage oder setzen der aktuellen Position
         */
		public PositionInfo Position {
			get { lock (infoLock) { return info.Position; } }
			set { lock (infoLock) { info.Position = value; } }
		}

        /**
         * Property Power [TRUE/FALSE], damit kann man beide Motoren gleichzeitig einschalten oder ausschalten
         */
		public bool Power { set { ctrl.Power = value; } }

        /**
         * Property DriveInfo gibt ein DriveInfo zurück.
         * @see DriveInfo
         */
		public DriveInfo Info { get { return info; } }

        /**
         * Property Done gibt Aufschluss dar&uuml;ber, ob eine zu fahrende Strecke absolviert ist.
         */
		public bool Done { get { return track == null; } }

        /**
         * Konstruktor f&uuml;r ein Drive Objekt.
         * 
         * @param runMode enum RunMode
         */
		public Drive(RunMode runMode)
		{
			if (!Config.IsWinCE)
				runMode = RunMode.VIRTUAL;
			if (runMode == RunMode.REAL)
			{
				ctrl = new DriveCtrl_HW(Config.IODriveCtrl);
				left = new MotorCtrl_HW(Config.IOMotorCtrlLeft);
				right = new MotorCtrl_HW(Config.IOMotorCtrlRight);
			}
			else
			{
				ctrl = new DriveCtrl();
				left = new MotorCtrl();
				right = new MotorCtrl();
			}
			lock (drivesLock)
			{
				left.Acceleration = 10.0;
				right.Acceleration = 10.0;
			}
            if(track == null)
                track = new Track();
			runTracksThread = new Thread(RunTracks);
			runTracksThread.Priority = ThreadPriority.AboveNormal;
			runTracksThread.Start();
		}

        /**
         * Property Distance zum setzen einer Distanz
         */
        public double Distance
        {
            set
            {
                if (track == null)
                {
                    track = new Track();
                }
                track.Distance = value; }
        }

        /**
         * Methode resetiert das referenzierte DriveCtrl
         */
		public void Reset()
		{
			ctrl.Reset();
		}

        /**
         * Methode resetiert das referenzierte DriveCtrl und beendet die aktuelle Fahrt des Robot.
         */
		public void Close()
		{
			ctrl.Reset();
			lock (drivesLock)
			{
				left.Close();
				right.Close();
			}
			runTracksThread.Abort();
		}

        /**
         * Befehl zum Stoppen des Robot.
         */
		public void Stop()
		{
			stop = true;
		}

        /**
         * Befehl zum Halten des Robot.
         */
		public void Halt()
		{
			halt = true;
		}


        /**
         * Zyklisches warten auf beendigung der Fahrt.
         */
		public void WaitDone()
		{
			while (!Done) Thread.Sleep(100);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es eine Pause.
         * 
         * @param pauseTimeSeconds Pause in Sekunden setzen
         */
		public void RunPause(double pauseTimeSeconds)
		{
			if (track == null)
				track = new TrackPause(pauseTimeSeconds);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es eine gerade Strecke.
         * 
         * @param length die zu fahrende Strecke in Meter
         * @param speed die Geschwindigket f&uuml;r die zu fahrende Strecke
         * @param runAcceleration die Beschleunigung auf der Strecke
         */
        public void RunLine(double length, double speed, double runAcceleration)
		{
			if (track == null)
				track = new TrackLine(length, speed, runAcceleration);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es einen Bogen nach links.
         * 
         * @param radius der Radius des zu fahrenden Bogens
         * @param angle der Winkel des zu fahrenden Bogens
         * @param speed die Geschwindigkeit f&uuml;r die zu fahrende Strecke
         * @param runAcceleration die Beschleunigung auf der Strecke
         */
        public void RunArcLeft(double radius, double angle, double speed, double runAcceleration)
		{
			if (track== null)
				track = new TrackArcLeft(radius, angle, speed, runAcceleration);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es einen Bogen nach rechts.
         * 
         * @param radius der Radius des zu fahrenden Bogens
         * @param angle der Winkel des zu fahrenden Bogens
         * @param speed die Geschwindigkeit f&uuml;r die zu fahrende Strecke
         * @param runAcceleration die Beschleunigung auf der Strecke
         */
        public void RunArcRight(double runRadius, double runAngle, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackArcRight(runRadius, runAngle, runSpeed, runAcceleration);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es eine Drehung um die eigene Achse.
         * 
         * @param runAngle der Winkel der Drehung
         * @param runSpeed die Geschwindigkeit f&uuml;r die zu fahrende Strecke
         * @param runAcceleration die Beschleunigung auf der Strecke
         */
		public void RunTurn(double runAngle, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackTurn(runAngle, runSpeed, runAcceleration);
		}

        /**
         * Methode setzt die zu fahrende Track. In diesem Fall ist es eine Fahrt um eine Kontur.
         * 
         * @param distance der Abstand zur Kontur
         * @param runSpeed die Geschwindigkeit f&uuml;r die zu fahrende Strecke
         * @param runAcceleration die Beschleunigung auf der Strecke
         */
		public void RunContourLeft(double distance, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackContourLeft(distance, runSpeed, runAcceleration);
		}

        /**
         * Befehl zum Fahren der gesetzten Strecke
         */
		void RunTracks()
		{
			double velocity = 0;
			double deltaTime;

			int ticks = Environment.TickCount;
			stop = false;
			halt = false;

			while (true)
			{
				// Möglichst schneller Process Control Loop
				// ----------------------------------------
				Thread.Sleep(1);

				// Stopp
				if (stop)
				{
					track = null;
					stop = false;
				}

				// neuen Track starten?
				if (track != oldTrack)
				{
					if (track != null)
					{
						// Intitialisierung
						// ------------------------
						lock (infoLock)
						{
							lock (drivesLock)
							{
								oldInfo.DistanceL = -left.Distance;
								oldInfo.DistanceR = right.Distance;
							}
							info.Runtime = 0;
						}
						track.Start(info.Position);
					}
				}
				oldTrack = track;

				// Aktuelle Prozessdaten erfassen
				// ------------------------------
				// Zeit
				int deltaTicks = Environment.TickCount - ticks;
				ticks += deltaTicks;
				deltaTime = deltaTicks / 1000.0;

				if (track != null)
				{
					if ((track.Done) || ((halt && (velocity == 0))))
					{
						track = null;
						halt = false;
					}
					else if (track.ResidualLength > 0)
					{
						// Neue Prozessparameter berechnen
						// -------------------------------
						if (halt)
						{
							velocity = Math.Max(0, velocity - deltaTime * track.acceleration);
						}
						else
						{
                            // Restweg
                            double restweg = track.ResidualLength;

							// Beschleunigung
                            double beschleunigung = track.acceleration;

                            // aktuelle Geschwindigkeit
                            double absoluteSpeed = track.nominalSpeed; /* aktuelle Geschwindigket */
                            absoluteSpeed += deltaTime * track.acceleration; /* +Geschwindigkeitszuwachs */

                            // Verzögerung auf Zielposition hin
                            double bremsgeschwindigkeit = Math.Sqrt(2 * beschleunigung * restweg);
							
                            // Begrenzung auf Reisegeschwindigkeit
                            double tmp = Math.Min(track.nominalSpeed, absoluteSpeed);

                            // Geschwindigkeit auf max. zulässige Bremsgeschwindigkeit limitieren
                            velocity = Math.Min(tmp, bremsgeschwindigkeit);

							//??????????????????????????????????
							//throw new ApplicationException("Ihre Ergänzung in Drive.RunTracks fehlt.");
							//??????????????????????????????????                           
						}

						// Neue Prozessparameter aktivieren
						// --------------------------------
						track.SetSpeed(velocity, left, right);
						left.Go();
						right.Go();
						track.Step(deltaTime);
					}
					else
					{
						track = null;
					}
				}
				else
				{
					// Idle-Zustand setzen
					// -------------------
					lock (drivesLock)
					{
						left.Speed = 0;
						right.Speed = 0;
						right.Go();
						left.Go();
					}
				}
				// Aktuellen Status sichern
				// ------------------------
				updateInfo(deltaTime);
			}
		}

        /**
         * Methode synchronisiert die lokale Information der Position mit der Hardware Information der Position
         * 
         * @param timeInterval der gew&uuml;nschte Zeitabschnitt
         */
		void updateInfo(double timeInterval)
		{
			// Motor-Status
			info.DriveStatus = ctrl.Status;

			lock (infoLock)
			{
				lock (drivesLock)
				{
					info.MotorStatusL = left.Status;
					info.MotorStatusR = right.Status;

					info.SpeedL = -left.Speed;
					info.SpeedR = right.Speed;
					info.DistanceL = -left.Distance;
					info.DistanceR = right.Distance;
				}
				if (track != null)
					info.Runtime = track.elapsedTime;
			}

			// Position und Richtung im Weltkoordinatensystem bestimmen 
			// --------------------------------------------------------

			double dL = info.DistanceL - oldInfo.DistanceL;
			double dR = info.DistanceR - oldInfo.DistanceR;

			double x1 = info.Position.X;
			double y1 = info.Position.Y;
			double phi1 = info.Position.Angle/180.0*Math.PI;

			double x2 = 0;
			double y2 = 0;
			double phi2 = 0;
			if (dL == dR)
			{
				//??????????????????????????????????
				//throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
                double d = (dL + dR) / 2;
                x2 = x1 + d * Math.Cos(phi1);
                y2 = y1 + d * Math.Sin(phi1);
                phi2 = phi1;
			}
			else if (dL == -dR)
			{
				//??????????????????????????????????
				//throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
                x2 = x1;
                y2 = y1;
                phi2 = phi1 + dR / (Config.AxleLength / 2);
			}
			else
			{
				//??????????????????????????????????
				//throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
                x2 = x1;
                y2 = y1;
                phi2 = phi1 + dL / (Config.AxleLength / 2);
			}
			lock (infoLock)
			{
				info.Position.X = x2;
				info.Position.Y = y2;
				info.Position.Angle = phi2 / Math.PI * 180;
				oldInfo = info;
			}
		}


		// private members
		// ---------------
		Track track = null;
		Track oldTrack = null;
		DriveCtrl ctrl;
		MotorCtrl left;
		MotorCtrl right;
		DriveInfo info;
		DriveInfo oldInfo;
		bool stop;
		bool halt;
		Object infoLock = new object();
		Object trackLock = new object();
		Thread runTracksThread;
		Object drivesLock = new object();
    }
}
