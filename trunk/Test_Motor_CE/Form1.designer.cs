namespace MotorTest
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.powerLeftCheckBox = new System.Windows.Forms.CheckBox();
			this.ctrlResetButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.powerRightCheckBox = new System.Windows.Forms.CheckBox();
			this.startButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.accelerationTrackBar = new System.Windows.Forms.TrackBar();
			this.label4 = new System.Windows.Forms.Label();
			this.accelerationTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.speedTrackBar = new System.Windows.Forms.TrackBar();
			this.speedTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.positionTextBox = new System.Windows.Forms.TextBox();
			this.positionResetButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.motorStatusTextBox = new System.Windows.Forms.TextBox();
			this.ctrlStatusTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(-1, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 21);
			this.label1.Text = "DriveCtrl-Reset";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// powerLeftCheckBox
			// 
			this.powerLeftCheckBox.Location = new System.Drawing.Point(237, 39);
			this.powerLeftCheckBox.Name = "powerLeftCheckBox";
			this.powerLeftCheckBox.Size = new System.Drawing.Size(24, 20);
			this.powerLeftCheckBox.TabIndex = 2;
			this.powerLeftCheckBox.Click += new System.EventHandler(this.powerLeftCheckBox_Click);
			// 
			// ctrlResetButton
			// 
			this.ctrlResetButton.Location = new System.Drawing.Point(237, 12);
			this.ctrlResetButton.Name = "ctrlResetButton";
			this.ctrlResetButton.Size = new System.Drawing.Size(54, 20);
			this.ctrlResetButton.TabIndex = 3;
			this.ctrlResetButton.Text = "reset";
			this.ctrlResetButton.Click += new System.EventHandler(this.ctrlResetButton_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(-1, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(162, 21);
			this.label2.Text = "Power left/right - Status";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// powerRightCheckBox
			// 
			this.powerRightCheckBox.Location = new System.Drawing.Point(267, 39);
			this.powerRightCheckBox.Name = "powerRightCheckBox";
			this.powerRightCheckBox.Size = new System.Drawing.Size(24, 20);
			this.powerRightCheckBox.TabIndex = 2;
			this.powerRightCheckBox.Click += new System.EventHandler(this.powerRightCheckBox_Click);
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(173, 231);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(72, 29);
			this.startButton.TabIndex = 6;
			this.startButton.Text = "Start";
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.Location = new System.Drawing.Point(270, 231);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(72, 29);
			this.stopButton.TabIndex = 6;
			this.stopButton.Text = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// accelerationTrackBar
			// 
			this.accelerationTrackBar.Location = new System.Drawing.Point(173, 126);
			this.accelerationTrackBar.Maximum = 5000;
			this.accelerationTrackBar.Name = "accelerationTrackBar";
			this.accelerationTrackBar.Size = new System.Drawing.Size(169, 42);
			this.accelerationTrackBar.SmallChange = 100;
			this.accelerationTrackBar.TabIndex = 7;
			this.accelerationTrackBar.TickFrequency = 500;
			this.accelerationTrackBar.Value = 500;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(-1, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(162, 21);
			this.label4.Text = "HW-Acceleration (m/s^2)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// accelerationTextBox
			// 
			this.accelerationTextBox.Location = new System.Drawing.Point(348, 126);
			this.accelerationTextBox.Name = "accelerationTextBox";
			this.accelerationTextBox.ReadOnly = true;
			this.accelerationTextBox.Size = new System.Drawing.Size(55, 23);
			this.accelerationTextBox.TabIndex = 9;
			this.accelerationTextBox.Text = "textBox1";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(-1, 174);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(162, 21);
			this.label5.Text = "HW-Speed (m/s)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// speedTrackBar
			// 
			this.speedTrackBar.Location = new System.Drawing.Point(173, 174);
			this.speedTrackBar.Maximum = 2000;
			this.speedTrackBar.Minimum = -2000;
			this.speedTrackBar.Name = "speedTrackBar";
			this.speedTrackBar.Size = new System.Drawing.Size(169, 42);
			this.speedTrackBar.SmallChange = 100;
			this.speedTrackBar.TabIndex = 7;
			this.speedTrackBar.TickFrequency = 500;
			// 
			// speedTextBox
			// 
			this.speedTextBox.Location = new System.Drawing.Point(348, 174);
			this.speedTextBox.Name = "speedTextBox";
			this.speedTextBox.ReadOnly = true;
			this.speedTextBox.Size = new System.Drawing.Size(55, 23);
			this.speedTextBox.TabIndex = 9;
			this.speedTextBox.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(-1, 294);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162, 21);
			this.label3.Text = "Distanz (m)";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// positionTextBox
			// 
			this.positionTextBox.Location = new System.Drawing.Point(173, 294);
			this.positionTextBox.Name = "positionTextBox";
			this.positionTextBox.Size = new System.Drawing.Size(72, 23);
			this.positionTextBox.TabIndex = 13;
			this.positionTextBox.Text = "textBox2";
			// 
			// positionResetButton
			// 
			this.positionResetButton.Location = new System.Drawing.Point(267, 294);
			this.positionResetButton.Name = "positionResetButton";
			this.positionResetButton.Size = new System.Drawing.Size(72, 23);
			this.positionResetButton.TabIndex = 6;
			this.positionResetButton.Text = "reset";
			this.positionResetButton.Click += new System.EventHandler(this.positionResetButton_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(-1, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(162, 21);
			this.label6.Text = "Motor-Status";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// motorStatusTextBox
			// 
			this.motorStatusTextBox.Location = new System.Drawing.Point(348, 86);
			this.motorStatusTextBox.Name = "motorStatusTextBox";
			this.motorStatusTextBox.Size = new System.Drawing.Size(55, 23);
			this.motorStatusTextBox.TabIndex = 20;
			this.motorStatusTextBox.Text = "textBox2";
			// 
			// ctrlStatusTextBox
			// 
			this.ctrlStatusTextBox.Location = new System.Drawing.Point(348, 38);
			this.ctrlStatusTextBox.Name = "ctrlStatusTextBox";
			this.ctrlStatusTextBox.Size = new System.Drawing.Size(55, 23);
			this.ctrlStatusTextBox.TabIndex = 20;
			this.ctrlStatusTextBox.Text = "textBox2";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(420, 334);
			this.Controls.Add(this.ctrlStatusTextBox);
			this.Controls.Add(this.motorStatusTextBox);
			this.Controls.Add(this.positionTextBox);
			this.Controls.Add(this.speedTextBox);
			this.Controls.Add(this.accelerationTextBox);
			this.Controls.Add(this.speedTrackBar);
			this.Controls.Add(this.accelerationTrackBar);
			this.Controls.Add(this.positionResetButton);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.ctrlResetButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.powerRightCheckBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.powerLeftCheckBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "MotorTest";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox powerLeftCheckBox;
		private System.Windows.Forms.Button ctrlResetButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox powerRightCheckBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.TrackBar accelerationTrackBar;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox accelerationTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar speedTrackBar;
		private System.Windows.Forms.TextBox speedTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox positionTextBox;
		private System.Windows.Forms.Button positionResetButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox motorStatusTextBox;
		private System.Windows.Forms.TextBox ctrlStatusTextBox;
	}
}

