using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
//using System.Drawing;

/**
 * @mainpage Hochschule f&uuml;r Technik & Architektur, HTA Luzern 
 *
 * <center><H1> PRGSY Robot </H1></center>
 * @author David Malgiaritta
 * @author Sascha Waser
 * <br><br><br>
 * 
 * 
 * Im Modul Systemnahes Programmieren an der Hochschule f&uuml;r Technik & Architektur Luzern wird
 * w&auml;hrend eines Semesters ein spezielles Projekt durchgef&uuml;hrt. Ziel des Projektes ist es,
 * anhand einer industrienahen Hard- und Software Plattform, das .NET Framework und insbesondere
 * die Programmiersprache C# kennenzulernen. <br>
 * Der Code wird auf Windows XP/Vista/7 und Visual Studio 2008 entwickelt. Der Roboter hingegen wird
 * mit Windows CE und dem .NET Compact Framework betrieben. Diese plattform unabh&auml;ngige 
 * Software-Entwicklung stellt eine besondere Herausforderung an die Studierenden.
 * 
 * <br><H2> Technische Daten </H2>
 * <H3> Antrieb </H3>
 * <pre>
 * Getriebe-&Uuml;bersetzung                     14 : 1
 * Encoder-Perioden pro Motor-Umdrehung     512 
 * Ticks pro Encoder-Periode                4 
 * Ticks pro Rad-Umdrehung                  28'672 
 * Rad-Durchmesser                          76 mm 
 * Rad-Abstand (Achsl&auml;nge)                  257 mm
 * </pre>
 * <H3> Motorsteuerung </H3>
 * <pre>
 * Motor-IC                                 LM629 (8MHz-Clock)
 * LM629 Sample Intervall                   256us
 * Leistungsstufe                           L6206
 * </pre>
 * 
 * <img src="../pictures/PrgSYS_Robot.jpg" alt="Roboter picture">
 */

namespace RobotCtrl
{
    /**
     * Klasse Robot, dient als Basis f&uuml;r einen realen oder virtuellen Roboter
     * @brief Basisklasse f&uuml;r einen Roboter
     */

    public class Robot
	{
        /**
         * Property Console liefert eine Referenz auf die Console innerhalb des Robot Objektes
         * @see Console
         * 
         * @return Robot Console
         */ 
		public Console Console { get { return console; }}

        /**
         * Property Drive liefert eine Referenz auf Drive innerhalb des Robot Objektes
         * @see Drive
         * 
         * @return Drive f&uuml;r den Robot
         */ 
        public Drive Drive { get { return drive; } }

        /**
         * Property PositionInfo liefert oder setzt eine Referenz auf eine Postion
         */
        public PositionInfo PositionInfo
        {
            get { return drive.Position; }
            set { drive.Position = value; }
        }

        /**
         * Property Color liefert oder setzt eine Farbe des Robot
         */
        public Color Color { get { return color; } set { color = value; } }

        /**
         * Property Radar liefert eine Referenz auf Radar innerhalb des Robot Objektes
         * @see Radar
         * 
         * @return Radar f&uuml;r den Robot
         */ 
        public Radar Radar { get { return radar; } }

        /**
         * Konstruktor f&uuml;r die Klasse Robot
         * @see #RunMode
         * 
         * @param runMode argument vom Typ enum #RunMode in Config.
         */
        public Robot(RunMode runMode)
		{
			if (!Config.IsWinCE)
				runMode = RunMode.VIRTUAL;
			console = new Console(runMode);
            drive = new Drive(runMode);
            radar = new Radar(runMode);
            
            drive.Distance = radar.Distance;
		}

        public void RunPause(double pauseTimeSeconds)
        {
            drive.RunPause(pauseTimeSeconds);
        }

        public void RunLine(double length, double speed, double runAcceleration)
        {
            drive.RunLine(length, speed, runAcceleration);
        }

        public void RunArcLeft(double radius, double angle, double speed, double runAcceleration)
        {
            drive.RunArcLeft(radius, angle, speed, runAcceleration);
        }

        public void RunArcRight(double runRadius, double runAngle, double runSpeed, double runAcceleration)
        {
            drive.RunArcRight(runRadius, runAngle, runSpeed, runAcceleration);
        }

        public void RunTurn(double runAngle, double runSpeed, double runAcceleration)
        {
            drive.RunTurn(runAngle, runSpeed, runAcceleration);
        }

        public void RunContourLeft(double distance, double runSpeed, double runAcceleration)
        {
            drive.RunContourLeft(distance, runSpeed, runAcceleration);
        }

        public PositionInfo getPosition()
        {
            return drive.Position;
        }

        public double Distance
        {
            set { drive.Distance = value; }
        }

        /**
         * Methode sollte alle laufenden Threads dieses Robot Objekts beenden
         */
        public void Clear()
        {
            drive.Stop();
            drive.Close();
        }

        Console console;
        Drive drive;
        Radar radar;
        //PositionInfo positionInfo;
        Color color = Color.Azure;
	}
}
