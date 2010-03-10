using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RobotView
{
    /// <summary>
    /// Klasse die einen einzelnen Switch darstellt.
    /// Wird Verwendet um einen Switch des HSLU Roboters
    /// darzustellen.
    /// </summary>
    /// <c> 
    /// SwitchView switchView = new SwitchView();
    /// switchView.Location = new System.Drawing.Point(22 + 95, 2);
    /// switchView.SwitchChanged += SwitchChanged_Handler;
    /// </c>
    public class SwitchView : UserControl
    {
        /// <summary>
        /// Hier kann man einen EventHandler registrieren
        /// </summary>
        public event System.EventHandler SwitchChanged;

        /// <summary>
        /// Standardkonstruktor
        /// </summary>
        public SwitchView()
        {
            InitializeComponent();
            update();
        }

        private PictureBox pictureBox;
        bool state = false;

        /// <summary>
        /// Setzen oder abfragen des aktuellen Switch Status
        /// </summary>
        public bool State
        {
            get { return state; }
            set { state = value; update(); }
        }

        /// <summary>
        /// Anhand des aktuellen Status wird der Switch gesetzt
        /// </summary>
        void update()
        {
            pictureBox.Image = (state) ? Resource1.SWITCH_ON : Resource1.SWITCH_OFF;
        }

        /// <summary>
        /// Initialisiert die einzelnen Komponenten
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(20, 40);
            // 
            // SwitchView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pictureBox);
            this.Name = "SwitchView";
            this.Size = new System.Drawing.Size(20, 40);
            this.ResumeLayout(false);
            //
            // Mouse Event Listener
            //
            this.pictureBox.MouseDown +=new MouseEventHandler(SwitchView_MouseDown);
        }

        /// <summary>
        /// Maus Event Handler
        /// Bei einem Klick auf den Switch wird ausgewertet auf welchen
        /// Teil des Schalters geklickt wurde. Je nach dem wird dann ein
        /// ON oder OFF ermittelt.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">MouseEventArgs</param>
        void SwitchView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Y > 20)
                {
                    if (SwitchChanged != null)
                    {
                        SwitchChanged(this, new SwitchEventArg(true));
                    }
                    //MessageBox.Show("ON");
                }
                else
                {
                    if (SwitchChanged != null)
                    {
                        SwitchChanged(this, new SwitchEventArg(false));
                    }
                    //MessageBox.Show("OFF");
                }
            }
            //MessageBox.Show("SwitchView_MouseDown X:" + e.X + " Y: " + e.Y);
        }
    }

    /// <summary>
    /// Klasse implementiert einen EventArg, der einen boolean State
    /// des Switches repräsentiert
    /// </summary>
    public class SwitchEventArg : EventArgs
    {
        /// <summary> Property um den State abzufragen </summary>
        /// <returns> true or false </returns>
        public bool State
        {
            get { return state; }
        }

        bool state;

        /// <summary>
        /// Constructor um dem Event einen boolean State mitzugeben
        /// </summary>
        /// <param name="state"></param>
        public SwitchEventArg(bool state)
        {
            this.state = state;
        }
    }
}
