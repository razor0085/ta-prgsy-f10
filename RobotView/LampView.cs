using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RobotView
{
    public class LampView : UserControl
    {
        public bool State
        {
            get { return state; }
            set { state = value; update(); }
        }
        
        public LampView()
        {
            InitializeComponent();
            update();
        }

        private PictureBox pictureBox;
        bool state = false;

        void update()
        {
            pictureBox.Image = (state) ? Resource1.LAMP_ON : Resource1.LAMP_OFF;
        }

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
            this.pictureBox.Size = new System.Drawing.Size(20, 20);
            // 
            // LampView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pictureBox);
            this.Name = "LampView";
            this.Size = new System.Drawing.Size(20, 20);
            this.ResumeLayout(false);

        }
    }
}
