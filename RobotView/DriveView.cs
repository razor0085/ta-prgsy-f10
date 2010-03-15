using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;

namespace RobotView
{
	public partial class DriveView : UserControl
	{
		private DriveView()
		{
			InitializeComponent();
		}

		public DriveView(Drive drive)
			:this()
		{
			this.drive = drive;
			Timer timer = new Timer();
			timer.Interval = 200;
			timer.Tick += new EventHandler(timer_Tick);
			timer.Enabled = true;
		}

		void timer_Tick(object sender, EventArgs e)
		{
			DriveInfo info = drive.Info;
			ctrlStatusTextBox.Text = "0x" + info.DriveStatus.ToString("X4");
			leftStatusTextBox.Text = "0x" + info.MotorStatusL.ToString("X4");
			rightStatusTextBox.Text = "0x" + info.MotorStatusR.ToString("X4");
			speedLTextBox.Text = info.SpeedL.ToString("F3");
			speedRTextBox.Text = info.SpeedR.ToString("F3");
			positionLTextBox.Text = info.DistanceL.ToString("F3");
			positionRTextBox.Text = info.DistanceR.ToString("F3");
			xTextBox.Text = info.Position.X.ToString("F3");
			yTextBox.Text = info.Position.Y.ToString("F3");
			angleTextBox.Text = info.Position.Angle.ToString("F3");
			runtimeTextBox.Text = info.Runtime.ToString("F3");
		}

		void resetButton_Click(object sender, EventArgs e)
		{
			drive.Position = new PositionInfo(0, 0, 0);
		}

		Drive drive;
	}
}
