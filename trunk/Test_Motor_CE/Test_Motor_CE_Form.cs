using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;

namespace MotorTest
{
	public partial class Test_Motor_CE_Form : Form
	{
		RunMode runMode = RunMode.REAL;
		bool running = false;

		DriveCtrl ctrl;
		MotorCtrl motor;

		public Test_Motor_CE_Form()
		{
			InitializeComponent();

			// DriveCtrl und MotorCtrl für linken Motor instanzieren
			if (!Config.IsWinCE)
				runMode = RunMode.VIRTUAL;
			if (runMode == RunMode.REAL)
			{
				ctrl = new DriveCtrl_HW(Config.IODriveCtrl);
				motor = new MotorCtrl_HW(Config.IOMotorCtrlLeft);
			}
			else
			{
				ctrl = new DriveCtrl();
				motor = new MotorCtrl();
			}

			// Startwerte setzen
			ctrl.PowerLeft = true;
			motor.SetPID(100, 20, 1000, 1000, 1);
			motor.Acceleration = 0.5;

			// Timer für GUI-Aktualisierung starten
			Timer timer = new Timer();
			timer.Interval = 200;
			timer.Enabled = true;
			timer.Tick += new EventHandler(timer_Tick);
		}

		void timer_Tick(object sender, EventArgs e)
		{
			ctrlStatusTextBox.Text = "0x" + ctrl.Status.ToString("X4");
			powerLeftCheckBox.Checked = ctrl.PowerLeft;
			powerRightCheckBox.Checked = ctrl.PowerRight;

			motorStatusTextBox.Text = "0x" + motor.Status.ToString("X4");

			double acceleration = accelerationTrackBar.Value / 1000.0;
			accelerationTextBox.Text = acceleration.ToString("F3");
			double speed = speedTrackBar.Value / 1000.0;
			speedTextBox.Text = speed.ToString("F3");

			if (running)
			{
				motor.Speed = speed;
				motor.Go();
			}else{
				motor.Acceleration = acceleration;
				motor.SetPID(100, 20, 1000, 1000, 1);
			}

			positionTextBox.Text = motor.Distance.ToString("F3");
		}


		private void Form1_Closing(object sender, CancelEventArgs e)
		{
			motor.Close();
		}

		private void ctrlResetButton_Click(object sender, EventArgs e)
		{
			ctrl.Reset();
			motor.Reset();
		}

		private void powerLeftCheckBox_Click(object sender, EventArgs e)
		{
			ctrl.PowerLeft = ((CheckBox)sender).Checked;
		}

		private void powerRightCheckBox_Click(object sender, EventArgs e)
		{
			ctrl.PowerRight = ((CheckBox)sender).Checked;
		}

		private void positionResetButton_Click(object sender, EventArgs e)
		{
			motor.Distance = 0;
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			running = true;
			motor.Go();
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			running = false;
			motor.Stop();
		}
	}
}