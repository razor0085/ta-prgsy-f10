using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RobotCtrl;
using RobotView;

namespace DriveTest
{
	public partial class Test_Drive_CE_Form : Form
	{
		public Test_Drive_CE_Form()
		{
			InitializeComponent();

			robot = new Robot(RunMode.REAL);
			drive = robot.Drive;
			DriveView driveView = new DriveView(drive);
			this.Controls.Add(driveView);

			System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
			timer.Interval = 200;
			timer.Enabled = true;
			timer.Tick += new EventHandler(timer_Tick);

			drive.Power = true;
		}

		void timer_Tick(object sender, EventArgs e)
		{
			linStartButton.Enabled = drive.Done;
			arcStartButton.Enabled = drive.Done;
			turnStartButton.Enabled = drive.Done;
		}

		private void Form1_Closing(object sender, CancelEventArgs e)
		{
			drive.Close();
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			drive.Stop();
		}

		private void linStartButton_Click(object sender, EventArgs e)
		{
			double length = (double)linLengthUpDown.Value / 1000.0;
			double speed = (double)linSpeedUpDown.Value / 1000.0;
			double acceleration = (double)linAccelerationUpDown.Value / 1000.0;
			drive.RunLine(length, speed, acceleration);
		}

		private void arcStartButton_Click(object sender, EventArgs e)
		{
			double radius = (double)arcRadiusUpDown.Value / 1000.0;
			double angle = (double)arcAngleUpDown.Value;
			double speed = (double)arcSpeedUpDown.Value / 1000.0;
			double acceleration = (double)arcAccelerationUpDown.Value / 1000.0;
			if (arcLeftRadioButton.Checked)
				drive.RunArcLeft(radius, angle, speed, acceleration);
			else
				drive.RunArcRight(radius, angle, speed, acceleration);
		}

		private void turnStartButton_Click(object sender, EventArgs e)
		{
			double angle = (double)turnAngleUpDown.Value;
			double speed = (double)turnSpeedUpDown.Value / 1000.0;
			double acceleration = (double)turnAccelerationUpDown.Value / 1000.0;
			drive.RunTurn(angle, speed, acceleration);
		}

		Robot robot;
		Drive drive;
	}
}