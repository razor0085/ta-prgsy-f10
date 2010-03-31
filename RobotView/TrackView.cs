using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;

namespace RobotView
{
    public partial class TrackView : UserControl
    {
        Button linStartButton;
        Label label_RunArc, label_Angle2, label_Speed2, label_Acceleration2, label_Radius, label_Length, label_RunLine, label_Speed3, label_Acceleration3, label_Angle, label_Speed, label_Acceleration, label_Right, label_RunTurn, label_Left;
        NumericUpDown linLengthUpDown, linSpeedUpDown, linAccelerationUpDown, arcAccelerationUpDown, arcSpeedUpDown, arcRadiusUpDown, arcAngleUpDown, turnAccelerationUpDown, turnSpeedUpDown, turnAngleUpDown;
        Button stopButton, arcStartButton, turnStartButton;
        Panel runPanel, arcPanel, turnPanel;
        RadioButton arcRightRadioButton, arcLeftRadioButton;

        Drive drive;

        public TrackView(Drive drive)
            : this()
        {
            this.drive = drive;
        }

        public TrackView()
        {
            InitializeComponent();

            this.Controls.Add(getRunPanel());
            this.Controls.Add(getArcPanel());
            this.Controls.Add(getTurnPanel());
        }

        private Label getLabelRunLine()
        {
            if (label_RunLine == null)
            {
                label_RunLine = new Label();
                label_RunLine.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                label_RunLine.Location = new System.Drawing.Point(3, 0);
                label_RunLine.Name = "label9";
                label_RunLine.Size = new System.Drawing.Size(100, 20);
                label_RunLine.Text = "RunLine";
            }
            return label_RunLine;
        }

        private Label getLabelRunSpeed()
        {
            if (label_Speed3 == null)
            {
                label_Speed3 = new Label();
                label_Speed3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Speed3.Location = new System.Drawing.Point(13, 46);
                label_Speed3.Name = "label10";
                label_Speed3.Size = new System.Drawing.Size(172, 20);
                label_Speed3.Text = "Speed (+ mm/s)";
            }
            return label_Speed3;
        }

        private Label getLabelRunAcceleration()
        {
            if (label_Acceleration3 == null)
            {
                label_Acceleration3 = new Label();
                label_Acceleration3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Acceleration3.Location = new System.Drawing.Point(13, 75);
                label_Acceleration3.Name = "label11";
                label_Acceleration3.Size = new System.Drawing.Size(172, 20);
                label_Acceleration3.Text = "Acceleration (+ mm/s^2)";
            }
            return label_Acceleration3;
        }

        private NumericUpDown getLinLengthUpDown()
        {
            if (linLengthUpDown == null)
            {
                linLengthUpDown = new NumericUpDown();
                linLengthUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                linLengthUpDown.Location = new System.Drawing.Point(191, 16);
                linLengthUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
                linLengthUpDown.Minimum = new decimal(new int[] { 10000, 0, 0, -2147483648 });
                linLengthUpDown.Name = "linLengthUpDown";
                linLengthUpDown.Size = new System.Drawing.Size(91, 24);
                linLengthUpDown.TabIndex = 15;
                linLengthUpDown.Value = new decimal(new int[] { 4000, 0, 0, 0 });
            }
            return linLengthUpDown;
        }

        private NumericUpDown getLinSpeedUpDown()
        {
            if (linSpeedUpDown == null)
            {
                linSpeedUpDown = new NumericUpDown();
                linSpeedUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                linSpeedUpDown.Location = new System.Drawing.Point(191, 46);
                linSpeedUpDown.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
                linSpeedUpDown.Name = "linSpeedUpDown";
                linSpeedUpDown.Size = new System.Drawing.Size(91, 24);
                linSpeedUpDown.TabIndex = 16;
                linSpeedUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            }
            return linSpeedUpDown;
        }

        private NumericUpDown getLinAccelerationUpDown()
        {
            if (linAccelerationUpDown == null)
            {
                linAccelerationUpDown = new NumericUpDown();
                linAccelerationUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                linAccelerationUpDown.Location = new System.Drawing.Point(191, 75);
                linAccelerationUpDown.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
                linAccelerationUpDown.Name = "linAccelerationUpDown";
                linAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
                linAccelerationUpDown.TabIndex = 16;
                linAccelerationUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
            }
            return linAccelerationUpDown;
        }

        private Button getButtonStop()
        {
            if (stopButton == null)
            {
                stopButton = new Button();
                stopButton.Location = new System.Drawing.Point(247, 660);
                stopButton.Name = "stopButton";
                stopButton.Size = new System.Drawing.Size(92, 33);
                stopButton.TabIndex = 0;
                stopButton.Text = "Stop";
                stopButton.Click += new System.EventHandler(this.stopButton_Click);
            }
            return stopButton;
        }

        private Label getLabelLeft()
        {
            if (label_Left == null)
            {
                label_Left = new Label();
                label_Left.Location = new System.Drawing.Point(191, 9);
                label_Left.Name = "label7";
                label_Left.Size = new System.Drawing.Size(28, 20);
                label_Left.Text = "Left";
            }
            return label_Left;
        }

        private RadioButton getArcRightRadioButton()
        {
            if (arcRightRadioButton == null)
            {
                arcRightRadioButton = new RadioButton();
                arcRightRadioButton.Location = new System.Drawing.Point(236, 9);
                arcRightRadioButton.Name = "arcRightRadioButton";
                arcRightRadioButton.Size = new System.Drawing.Size(21, 20);
                arcRightRadioButton.TabIndex = 21;
                arcRightRadioButton.TabStop = false;
            }
            return arcRightRadioButton;
        }

        private RadioButton getArcLeftRadioButton()
        {
            if (arcLeftRadioButton == null)
            {
                arcLeftRadioButton = new RadioButton();
                arcLeftRadioButton.Checked = true;
                arcLeftRadioButton.Location = new System.Drawing.Point(218, 9);
                arcLeftRadioButton.Name = "arcLeftRadioButton";
                arcLeftRadioButton.Size = new System.Drawing.Size(20, 20);
                arcLeftRadioButton.TabIndex = 21;
            }
            return arcLeftRadioButton;
        }

        private Label getLabelRunArc()
        {
            if (label_RunArc == null)
            {
                label_RunArc = new Label();
                label_RunArc.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                label_RunArc.Location = new System.Drawing.Point(3, 0);
                label_RunArc.Name = "label1";
                label_RunArc.Size = new System.Drawing.Size(100, 20);
                label_RunArc.Text = "RunArc";
            }
            return label_RunArc;
        }

        private NumericUpDown getArcAccelerationUpDown()
        {
            if (arcAccelerationUpDown == null)
            {
                arcAccelerationUpDown = new NumericUpDown();
                arcAccelerationUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                arcAccelerationUpDown.Location = new System.Drawing.Point(191, 121);
                arcAccelerationUpDown.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
                arcAccelerationUpDown.Name = "arcAccelerationUpDown";
                arcAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
                arcAccelerationUpDown.TabIndex = 16;
                arcAccelerationUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
            }
            return arcAccelerationUpDown;
        }

        private Button getArcStartButton()
        {
            if (arcStartButton == null)
            {
                arcStartButton = new Button();
                arcStartButton.Location = new System.Drawing.Point(288, 121);
                arcStartButton.Name = "arcStartButton";
                arcStartButton.Size = new System.Drawing.Size(51, 24);
                arcStartButton.TabIndex = 0;
                arcStartButton.Text = "Start";
                arcStartButton.Click += new System.EventHandler(this.arcStartButton_Click);
            }
            return arcStartButton;
        }

        private NumericUpDown getArcSpeedUpDown()
        {
            if (arcSpeedUpDown == null)
            {
                arcSpeedUpDown = new NumericUpDown();
                arcSpeedUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                arcSpeedUpDown.Location = new System.Drawing.Point(191, 92);
                arcSpeedUpDown.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
                arcSpeedUpDown.Name = "arcSpeedUpDown";
                arcSpeedUpDown.Size = new System.Drawing.Size(91, 24);
                arcSpeedUpDown.TabIndex = 16;
                arcSpeedUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            }
            return arcSpeedUpDown;
        }

        private NumericUpDown getArcRadiusUpDown()
        {
            if (arcRadiusUpDown == null)
            {
                arcRadiusUpDown = new NumericUpDown();
                arcRadiusUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                arcRadiusUpDown.Location = new System.Drawing.Point(191, 32);
                arcRadiusUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
                arcRadiusUpDown.Name = "arcRadiusUpDown";
                arcRadiusUpDown.Size = new System.Drawing.Size(91, 24);
                arcRadiusUpDown.TabIndex = 15;
                arcRadiusUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            }
            return arcRadiusUpDown;
        }

        private Label getLabelRadius()
        {
            if (label_Radius == null)
            {
                label_Radius = new Label();
                label_Radius.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Radius.Location = new System.Drawing.Point(13, 36);
                label_Radius.Name = "label5";
                label_Radius.Size = new System.Drawing.Size(172, 20);
                label_Radius.Text = "Radius (+ mm)";
            }
            return label_Radius;
        }

        private NumericUpDown getArcAngleUpDown()
        {
            if (arcAngleUpDown == null)
            {
                arcAngleUpDown = new NumericUpDown();
                arcAngleUpDown.Increment = new decimal(new int[] { 5, 0, 0, 0 });
                arcAngleUpDown.Location = new System.Drawing.Point(191, 62);
                arcAngleUpDown.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
                arcAngleUpDown.Minimum = new decimal(new int[] { 360, 0, 0, -2147483648 });
                arcAngleUpDown.Name = "arcAngleUpDown";
                arcAngleUpDown.Size = new System.Drawing.Size(91, 24);
                arcAngleUpDown.TabIndex = 15;
                arcAngleUpDown.Value = new decimal(new int[] { 90, 0, 0, 0 });
            }
            return arcAngleUpDown;
        }

        private Label getLabelArcAngle()
        {
            if (label_Angle2 == null)
            {
                label_Angle2 = new Label();
                label_Angle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Angle2.Location = new System.Drawing.Point(13, 66);
                label_Angle2.Name = "label2";
                label_Angle2.Size = new System.Drawing.Size(172, 20);
                label_Angle2.Text = "Angle (+/- degrees)";
            }
            return label_Angle2;
        }

        private Label getLabelArcSpeed()
        {
            if (label_Speed2 == null)
            {
                label_Speed2 = new Label();
                label_Speed2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Speed2.Location = new System.Drawing.Point(13, 92);
                label_Speed2.Name = "label3";
                label_Speed2.Size = new System.Drawing.Size(172, 20);
                label_Speed2.Text = "Speed (+ mm/s)";
            }
            return label_Speed2;
        }

        private Label getLabelArcAcceleration()
        {
            if (label_Acceleration2 == null)
            {
                label_Acceleration2 = new Label();
                label_Acceleration2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Acceleration2.Location = new System.Drawing.Point(13, 121);
                label_Acceleration2.Name = "label4";
                label_Acceleration2.Size = new System.Drawing.Size(172, 20);
                label_Acceleration2.Text = "Acceleration (+ mm/s^2)";
            }
            return label_Acceleration2;
        }

        private Label getLabelRunTurn()
        {
            if (label_RunTurn == null)
            {
                label_RunTurn = new Label();
                label_RunTurn.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
                label_RunTurn.Location = new System.Drawing.Point(3, 0);
                label_RunTurn.Name = "label6";
                label_RunTurn.Size = new System.Drawing.Size(100, 20);
                label_RunTurn.Text = "RunTurn";
            }
            return label_RunTurn;
        }

        private NumericUpDown getTurnAccelerationUpDown()
        {
            if (turnAccelerationUpDown == null)
            {
                turnAccelerationUpDown = new NumericUpDown();
                turnAccelerationUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                turnAccelerationUpDown.Location = new System.Drawing.Point(191, 76);
                turnAccelerationUpDown.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
                turnAccelerationUpDown.Name = "turnAccelerationUpDown";
                turnAccelerationUpDown.Size = new System.Drawing.Size(91, 24);
                turnAccelerationUpDown.TabIndex = 16;
                turnAccelerationUpDown.Value = new decimal(new int[] { 500, 0, 0, 0 });
            }
            return turnAccelerationUpDown;
        }

        private Button getTurnStartButton()
        {
            if (turnStartButton == null)
            {
                turnStartButton = new Button();
                turnStartButton.Location = new System.Drawing.Point(288, 76);
                turnStartButton.Name = "turnStartButton";
                turnStartButton.Size = new System.Drawing.Size(51, 24);
                turnStartButton.TabIndex = 0;
                turnStartButton.Text = "Start";
                turnStartButton.Click += new System.EventHandler(this.turnStartButton_Click);
            }
            return turnStartButton;
        }

        private NumericUpDown getTurnSpeedUpDown()
        {
            if (turnSpeedUpDown == null)
            {
                turnSpeedUpDown = new NumericUpDown();
                turnSpeedUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
                turnSpeedUpDown.Location = new System.Drawing.Point(191, 47);
                turnSpeedUpDown.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
                turnSpeedUpDown.Name = "turnSpeedUpDown";
                turnSpeedUpDown.Size = new System.Drawing.Size(91, 24);
                turnSpeedUpDown.TabIndex = 16;
                turnSpeedUpDown.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            }
            return turnSpeedUpDown;
        }

        private NumericUpDown getTurnAngleUpDown()
        {
            if (turnAngleUpDown == null)
            {
                turnAngleUpDown = new NumericUpDown();
                turnAngleUpDown.Increment = new decimal(new int[] { 5, 0, 0, 0 });
                turnAngleUpDown.Location = new System.Drawing.Point(191, 17);
                turnAngleUpDown.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
                turnAngleUpDown.Minimum = new decimal(new int[] { 360, 0, 0, -2147483648 });
                turnAngleUpDown.Name = "turnAngleUpDown";
                turnAngleUpDown.Size = new System.Drawing.Size(91, 24);
                turnAngleUpDown.TabIndex = 15;
                turnAngleUpDown.Value = new decimal(new int[] { 90, 0, 0, 0 });
            }
            return turnAngleUpDown;
        }

        private Label getLabelSpeed()
        {
            if (label_Speed == null)
            {
                label_Speed = new Label();
                label_Speed.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Speed.Location = new System.Drawing.Point(13, 47);
                label_Speed.Name = "label13";
                label_Speed.Size = new System.Drawing.Size(172, 20);
                label_Speed.Text = "Speed (+ mm/s)";
            }
            return label_Speed;
        }

        private Label getLabelAcceleration()
        {
            if (label_Acceleration == null)
            {
                label_Acceleration = new Label();
                label_Acceleration.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Acceleration.Location = new System.Drawing.Point(13, 76);
                label_Acceleration.Name = "label14";
                label_Acceleration.Size = new System.Drawing.Size(172, 20);
                label_Acceleration.Text = "Acceleration (+ mm/s^2)";
            }
            return label_Acceleration;
        }

        private Label getLabelAngle()
        {
            if (label_Angle == null)
            {
                label_Angle = new Label();
                label_Angle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Angle.Location = new System.Drawing.Point(13, 21);
                label_Angle.Name = "label12";
                label_Angle.Size = new System.Drawing.Size(172, 20);
                label_Angle.Text = "Angle (+/- degrees)";
            }
            return label_Angle;
        }

        private Label getLabelLength()
        {
            if (label_Length == null)
            {
                label_Length = new Label(); 
                label_Length.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular);
                label_Length.Location = new System.Drawing.Point(13, 20);
                label_Length.Name = "label8";
                label_Length.Size = new System.Drawing.Size(172, 20);
                label_Length.Text = "Length (+/- mm)";
            }
            return label_Length;
        }

        private Label getLabelRight()
        {
            if (label_Right == null)
            {
                label_Right = new Label();
                label_Right.Location = new System.Drawing.Point(254, 9);
                label_Right.Name = "label15";
                label_Right.Size = new System.Drawing.Size(39, 20);
                label_Right.Text = "Right";
            }
            return label_Right;
        }

        private Panel getTurnPanel()
        {
            if (turnPanel == null)
            {
                turnPanel = new Panel();
                turnPanel.Controls.Add(getLabelRunTurn());
                turnPanel.Controls.Add(getTurnAccelerationUpDown());
                turnPanel.Controls.Add(getTurnStartButton());
                turnPanel.Controls.Add(getTurnSpeedUpDown());
                turnPanel.Controls.Add(getTurnAngleUpDown());
                turnPanel.Controls.Add(getLabelAngle());
                turnPanel.Controls.Add(getLabelSpeed());
                turnPanel.Controls.Add(getLabelAcceleration());
                turnPanel.Location = new System.Drawing.Point(0, 546);
                turnPanel.Name = "turnPanel";
                turnPanel.Size = new System.Drawing.Size(346, 108);
            }
            return turnPanel;
        }

        private Panel getRunPanel()
        {
            if (runPanel == null)
            {
                runPanel = new Panel();
                runPanel.Controls.Add(getLabelRunLine());
                runPanel.Controls.Add(getLinAccelerationUpDown());
                runPanel.Controls.Add(getLinStartButton());
                runPanel.Controls.Add(getLinSpeedUpDown());
                runPanel.Controls.Add(getLinLengthUpDown());
                runPanel.Controls.Add(getLabelLength());
                runPanel.Controls.Add(getLabelRunSpeed());
                runPanel.Controls.Add(getLabelRunAcceleration());
                runPanel.Location = new System.Drawing.Point(0, 290);
                runPanel.Name = "runPanel";
                runPanel.Size = new System.Drawing.Size(346, 108);
            }
            return runPanel;
        }

        private Panel getArcPanel()
        {
            if (arcPanel == null)
            {
                arcPanel = new Panel();
                arcPanel.Controls.Add(getLabelRight());
                arcPanel.Controls.Add(getLabelLeft());
                arcPanel.Controls.Add(getArcRightRadioButton());
                arcPanel.Controls.Add(getArcLeftRadioButton());
                arcPanel.Controls.Add(getLabelRunArc());
                arcPanel.Controls.Add(getArcAccelerationUpDown());
                arcPanel.Controls.Add(getArcStartButton());
                arcPanel.Controls.Add(getArcSpeedUpDown());
                arcPanel.Controls.Add(getArcRadiusUpDown());
                arcPanel.Controls.Add(getLabelRadius());
                arcPanel.Controls.Add(getArcAngleUpDown());
                arcPanel.Controls.Add(getLabelArcAngle());
                arcPanel.Controls.Add(getLabelArcSpeed());
                arcPanel.Controls.Add(getLabelArcAcceleration());
                arcPanel.Location = new System.Drawing.Point(0, 395);
                arcPanel.Name = "arcPanel";
                arcPanel.Size = new System.Drawing.Size(346, 149);
            }
            return arcPanel;
        }

        private Button getLinStartButton()
        {
            if (linStartButton == null)
            {
                linStartButton = new System.Windows.Forms.Button();
                linStartButton.Location = new System.Drawing.Point(288, 75);
                linStartButton.Name = "linStartButton";
                linStartButton.Size = new System.Drawing.Size(51, 24);
                linStartButton.TabIndex = 0;
                linStartButton.Text = "Start";
                linStartButton.Click += new System.EventHandler(this.linStartButton_Click);
            }
            return linStartButton;
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

        private void stopButton_Click(object sender, EventArgs e)
        {
            drive.Stop();
        }
    }
}
