using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RobotCtrl
{
	public class Drive
	{
		public PositionInfo Position {
			get { lock (infoLock) { return info.Position; } }
			set { lock (infoLock) { info.Position = value; } }
		}
		public bool Power { set { ctrl.Power = value; } }
		public DriveInfo Info { get { return info; } }
		public bool Done { get { return track == null; } }

		public Drive(Robot robot, RunMode runMode)
		{
			this.robot = robot;

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
			runTracksThread = new Thread(RunTracks);
			runTracksThread.Priority = ThreadPriority.AboveNormal;
			runTracksThread.Start();
		}

		public void Reset()
		{
			ctrl.Reset();
		}

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

		public void Stop()
		{
			stop = true;
		}

		public void Halt()
		{
			halt = true;
		}

		public void WaitDone()
		{
			while (!Done) Thread.Sleep(100);
		}

		public void RunPause(double pauseTimeSeconds)
		{
			if (track == null)
				track = new TrackPause(pauseTimeSeconds);
		}

		public void RunLine(double length, double speed, double acceleration)
		{
			if (track == null)
				track = new TrackLine(length, speed, acceleration);
		}

		public void RunArcLeft(double radius, double angle, double speed, double acceleration)
		{
			if (track== null)
				track = new TrackArcLeft(radius, angle, speed, acceleration);
		}

		public void RunArcRight(double runRadius, double runAngle, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackArcRight(runRadius, runAngle, runSpeed, runAcceleration);
		}

		public void RunTurn(double runAngle, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackTurn(runAngle, runSpeed, runAcceleration);
		}

		public void RunContourLeft(double distance, double runSpeed, double runAcceleration)
		{
			if (track == null)
				track = new TrackContourLeft(distance, runSpeed, runAcceleration);
		}

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
							// Beschleunigung
							
							// Begrenzung auf Reisegeschwindigkeit

							// Verzögerung auf Zielposition hin
							// Geschwindigkeit auf max. zulässige Bremsgeschwindigkeit limitieren


							//??????????????????????????????????
							throw new ApplicationException("Ihre Ergänzung in Drive.RunTracks fehlt.");
							//??????????????????????????????????

							//velocity = ??
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
				throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
			}
			else if (dL == -dR)
			{
				//??????????????????????????????????
				throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
			}
			else
			{
				//??????????????????????????????????
				throw new ApplicationException("Ihre Ergänzung in Drive.updateInfo fehlt.");
				//??????????????????????????????????
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
		Robot robot;
		Object drivesLock = new object();
	}
}
