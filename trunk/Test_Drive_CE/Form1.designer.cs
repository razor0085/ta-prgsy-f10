namespace DriveTest
{
	partial class Form1
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.linStartButton = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.linLengthUpDown = new System.Windows.Forms.NumericUpDown();
			this.linSpeedUpDown = new System.Windows.Forms.NumericUpDown();
			this.linAccelerationUpDown = new System.Windows.Forms.NumericUpDown();
			this.stopButton = new System.Windows.Forms.Button();
			this.runPanel = new System.Windows.Forms.Panel();
			this.arcPanel = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.arcRightRadioButton = new System.Windows.Forms.RadioButton();
			this.arcLeftRadioButton = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.arcAccelerationUpDown = new System.Windows.Forms.NumericUpDown();
			this.arcStartButton = new System.Windows.Forms.Button();
			this.arcSpeedUpDown = new System.Windows.Forms.NumericUpDown();
			this.arcRadiusUpDown = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.arcAngleUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.turnPanel = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.turnAccelerationUpDown = new System.Windows.Forms.NumericUpDown();
			this.turnStartButton = new System.Windows.Forms.Button();
			this.turnSpeedUpDown = new System.Windows.Forms.NumericUpDown();
			this.turnAngleUpDown = new System.Windows.Forms.NumericUpDown();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.runPanel.SuspendLayout();
			this.arcPanel.SuspendLayout();
			this.turnPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// linStartButton
			// 
			this.linStartButton.Location = new System.Drawing.Point(288, 75);
			this.linStartButton.Name = "linStartButton";
			this.linStartButton.Size = new System.Drawing.Size(51, 24);
			this.linStartButton.TabIndex = 0;
			this.linStartButton.Text = "Start";
			this.linStartButton.Click += new System.EventHandler(this.linStartButton_Click);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label8.Location = new System.Drawing.Point(13, 20);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(172, 20);
			this.label8.Text = "Length (+/- mm)";
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(3, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 20);
			this.label9.Text = "RunLine";
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label10.Location = new System.Drawing.Point(13, 46);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(172, 20);
			this.label10.Text = "Speed (+ mm/s)";
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label11.Location = new System.Drawing.Point(13, 75);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(172, 20);
			this.label11.Text = "Acceleration (+ mm/s^2)";
			// 
			// linLengthUpDown
			// 
			this.linLengthUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.linLengthUpDown.Location = new System.Drawing.Point(191, 16);
			this.linLengthUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.linLengthUpDown.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.linLengthUpDown.Name = "linLengthUpDown";
			this.linLengthUpDown.Size = new System.Drawing.Size(91, 24);
			this.linLengthUpDown.TabIndex = 15;
			this.linLengthUpDown.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
			// 
			// linSpeedUpDown
			// 
			this.linSpeedUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.linSpeedUpDown.Location = new System.Drawing.Point(191, 46);
			this.linSpeedUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.linSpeedUpDown.Name = "linSpeedUpDown";
			this.linSpeedUpDown.Size = new System.Drawing.Size(91, 24);
			this.linSpeedUpDown.TabIndex = 16;
			this.linSpeedUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// linAccelerationUpDown
			// 
			this.linAccelerationUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.linAccelerationUpDown.Location = new System.Drawing.Point(191, 75);
			this.linAccelerationUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.linAccelerationUpDown.Name = "linAccelerationUpDown";
			this.linAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
			this.linAccelerationUpDown.TabIndex = 16;
			this.linAccelerationUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// stopButton
			// 
			this.stopButton.Location = new System.Drawing.Point(247, 660);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(92, 33);
			this.stopButton.TabIndex = 0;
			this.stopButton.Text = "Stop";
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// runPanel
			// 
			this.runPanel.Controls.Add(this.label9);
			this.runPanel.Controls.Add(this.linAccelerationUpDown);
			this.runPanel.Controls.Add(this.linStartButton);
			this.runPanel.Controls.Add(this.linSpeedUpDown);
			this.runPanel.Controls.Add(this.linLengthUpDown);
			this.runPanel.Controls.Add(this.label8);
			this.runPanel.Controls.Add(this.label10);
			this.runPanel.Controls.Add(this.label11);
			this.runPanel.Location = new System.Drawing.Point(0, 290);
			this.runPanel.Name = "runPanel";
			this.runPanel.Size = new System.Drawing.Size(346, 108);
			// 
			// arcPanel
			// 
			this.arcPanel.Controls.Add(this.label15);
			this.arcPanel.Controls.Add(this.label7);
			this.arcPanel.Controls.Add(this.arcRightRadioButton);
			this.arcPanel.Controls.Add(this.arcLeftRadioButton);
			this.arcPanel.Controls.Add(this.label1);
			this.arcPanel.Controls.Add(this.arcAccelerationUpDown);
			this.arcPanel.Controls.Add(this.arcStartButton);
			this.arcPanel.Controls.Add(this.arcSpeedUpDown);
			this.arcPanel.Controls.Add(this.arcRadiusUpDown);
			this.arcPanel.Controls.Add(this.label5);
			this.arcPanel.Controls.Add(this.arcAngleUpDown);
			this.arcPanel.Controls.Add(this.label2);
			this.arcPanel.Controls.Add(this.label3);
			this.arcPanel.Controls.Add(this.label4);
			this.arcPanel.Location = new System.Drawing.Point(0, 395);
			this.arcPanel.Name = "arcPanel";
			this.arcPanel.Size = new System.Drawing.Size(346, 149);
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(254, 9);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(39, 20);
			this.label15.Text = "Right";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(191, 9);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(28, 20);
			this.label7.Text = "Left";
			// 
			// arcRightRadioButton
			// 
			this.arcRightRadioButton.Location = new System.Drawing.Point(236, 9);
			this.arcRightRadioButton.Name = "arcRightRadioButton";
			this.arcRightRadioButton.Size = new System.Drawing.Size(21, 20);
			this.arcRightRadioButton.TabIndex = 21;
			this.arcRightRadioButton.TabStop = false;
			// 
			// arcLeftRadioButton
			// 
			this.arcLeftRadioButton.Checked = true;
			this.arcLeftRadioButton.Location = new System.Drawing.Point(218, 9);
			this.arcLeftRadioButton.Name = "arcLeftRadioButton";
			this.arcLeftRadioButton.Size = new System.Drawing.Size(20, 20);
			this.arcLeftRadioButton.TabIndex = 21;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.Text = "RunArc";
			// 
			// arcAccelerationUpDown
			// 
			this.arcAccelerationUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.arcAccelerationUpDown.Location = new System.Drawing.Point(191, 121);
			this.arcAccelerationUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.arcAccelerationUpDown.Name = "arcAccelerationUpDown";
			this.arcAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
			this.arcAccelerationUpDown.TabIndex = 16;
			this.arcAccelerationUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// arcStartButton
			// 
			this.arcStartButton.Location = new System.Drawing.Point(288, 121);
			this.arcStartButton.Name = "arcStartButton";
			this.arcStartButton.Size = new System.Drawing.Size(51, 24);
			this.arcStartButton.TabIndex = 0;
			this.arcStartButton.Text = "Start";
			this.arcStartButton.Click += new System.EventHandler(this.arcStartButton_Click);
			// 
			// arcSpeedUpDown
			// 
			this.arcSpeedUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.arcSpeedUpDown.Location = new System.Drawing.Point(191, 92);
			this.arcSpeedUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.arcSpeedUpDown.Name = "arcSpeedUpDown";
			this.arcSpeedUpDown.Size = new System.Drawing.Size(91, 24);
			this.arcSpeedUpDown.TabIndex = 16;
			this.arcSpeedUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// arcRadiusUpDown
			// 
			this.arcRadiusUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.arcRadiusUpDown.Location = new System.Drawing.Point(191, 32);
			this.arcRadiusUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.arcRadiusUpDown.Name = "arcRadiusUpDown";
			this.arcRadiusUpDown.Size = new System.Drawing.Size(91, 24);
			this.arcRadiusUpDown.TabIndex = 15;
			this.arcRadiusUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label5.Location = new System.Drawing.Point(13, 36);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(172, 20);
			this.label5.Text = "Radius (+ mm)";
			// 
			// arcAngleUpDown
			// 
			this.arcAngleUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.arcAngleUpDown.Location = new System.Drawing.Point(191, 62);
			this.arcAngleUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.arcAngleUpDown.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.arcAngleUpDown.Name = "arcAngleUpDown";
			this.arcAngleUpDown.Size = new System.Drawing.Size(91, 24);
			this.arcAngleUpDown.TabIndex = 15;
			this.arcAngleUpDown.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label2.Location = new System.Drawing.Point(13, 66);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(172, 20);
			this.label2.Text = "Angle (+/- degrees)";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label3.Location = new System.Drawing.Point(13, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(172, 20);
			this.label3.Text = "Speed (+ mm/s)";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(13, 121);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(172, 20);
			this.label4.Text = "Acceleration (+ mm/s^2)";
			// 
			// turnPanel
			// 
			this.turnPanel.Controls.Add(this.label6);
			this.turnPanel.Controls.Add(this.turnAccelerationUpDown);
			this.turnPanel.Controls.Add(this.turnStartButton);
			this.turnPanel.Controls.Add(this.turnSpeedUpDown);
			this.turnPanel.Controls.Add(this.turnAngleUpDown);
			this.turnPanel.Controls.Add(this.label12);
			this.turnPanel.Controls.Add(this.label13);
			this.turnPanel.Controls.Add(this.label14);
			this.turnPanel.Location = new System.Drawing.Point(0, 546);
			this.turnPanel.Name = "turnPanel";
			this.turnPanel.Size = new System.Drawing.Size(346, 108);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 20);
			this.label6.Text = "RunTurn";
			// 
			// turnAccelerationUpDown
			// 
			this.turnAccelerationUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.turnAccelerationUpDown.Location = new System.Drawing.Point(191, 76);
			this.turnAccelerationUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.turnAccelerationUpDown.Name = "turnAccelerationUpDown";
			this.turnAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
			this.turnAccelerationUpDown.TabIndex = 16;
			this.turnAccelerationUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
			// 
			// turnStartButton
			// 
			this.turnStartButton.Location = new System.Drawing.Point(288, 76);
			this.turnStartButton.Name = "turnStartButton";
			this.turnStartButton.Size = new System.Drawing.Size(51, 24);
			this.turnStartButton.TabIndex = 0;
			this.turnStartButton.Text = "Start";
			this.turnStartButton.Click += new System.EventHandler(this.turnStartButton_Click);
			// 
			// turnSpeedUpDown
			// 
			this.turnSpeedUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.turnSpeedUpDown.Location = new System.Drawing.Point(191, 47);
			this.turnSpeedUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.turnSpeedUpDown.Name = "turnSpeedUpDown";
			this.turnSpeedUpDown.Size = new System.Drawing.Size(91, 24);
			this.turnSpeedUpDown.TabIndex = 16;
			this.turnSpeedUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// turnAngleUpDown
			// 
			this.turnAngleUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.turnAngleUpDown.Location = new System.Drawing.Point(191, 17);
			this.turnAngleUpDown.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.turnAngleUpDown.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.turnAngleUpDown.Name = "turnAngleUpDown";
			this.turnAngleUpDown.Size = new System.Drawing.Size(91, 24);
			this.turnAngleUpDown.TabIndex = 15;
			this.turnAngleUpDown.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label12.Location = new System.Drawing.Point(13, 21);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(172, 20);
			this.label12.Text = "Angle (+/- degrees)";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label13.Location = new System.Drawing.Point(13, 47);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(172, 20);
			this.label13.Text = "Speed (+ mm/s)";
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
			this.label14.Location = new System.Drawing.Point(13, 76);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(172, 20);
			this.label14.Text = "Acceleration (+ mm/s^2)";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(349, 696);
			this.Controls.Add(this.turnPanel);
			this.Controls.Add(this.arcPanel);
			this.Controls.Add(this.runPanel);
			this.Controls.Add(this.stopButton);
			this.Name = "Form1";
			this.Text = "DriveTest";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.runPanel.ResumeLayout(false);
			this.arcPanel.ResumeLayout(false);
			this.turnPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button linStartButton;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown linLengthUpDown;
		private System.Windows.Forms.NumericUpDown linSpeedUpDown;
		private System.Windows.Forms.NumericUpDown linAccelerationUpDown;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Panel runPanel;
		private System.Windows.Forms.Panel arcPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown arcAccelerationUpDown;
		private System.Windows.Forms.Button arcStartButton;
		private System.Windows.Forms.NumericUpDown arcSpeedUpDown;
		private System.Windows.Forms.NumericUpDown arcAngleUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown arcRadiusUpDown;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel turnPanel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown turnAccelerationUpDown;
		private System.Windows.Forms.Button turnStartButton;
		private System.Windows.Forms.NumericUpDown turnSpeedUpDown;
		private System.Windows.Forms.NumericUpDown turnAngleUpDown;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton arcRightRadioButton;
		private System.Windows.Forms.RadioButton arcLeftRadioButton;
	}
}
