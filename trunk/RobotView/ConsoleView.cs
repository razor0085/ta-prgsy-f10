using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;

namespace RobotView
{
    public class ConsoleView : UserControl
    {
        Timer timer;
        RobotCtrl.Console robotConsole;

        LampView[] lampView = new LampView[4];
        SwitchView[] switchView = new SwitchView[4];
        Label[] label = new Label[4];

        public ConsoleView()
        {
            //InitializeComponent();
            this.Size = new System.Drawing.Size(183, 45);
            this.BackColor = System.Drawing.Color.Black;

            for (int i = 0; i < 4; i++)
            {
                lampView[i] = new LampView();
                lampView[i].Location = new System.Drawing.Point(i * 22 + 2, 2);
                switchView[i] = new SwitchView();
                switchView[i].Location = new System.Drawing.Point(i * 22 + 95, 2);
                switchView[i].SwitchChanged += SwitchChanged_Handler;

                label[i] = new Label();
                label[i].Location = new System.Drawing.Point(i * 22 + 5, 27);
                label[i].Text = (i + 1).ToString();
                label[i].ForeColor = System.Drawing.Color.White;
                label[i].Size = new System.Drawing.Size(10, 12);
                label[i].Name = "label" + (i + 1).ToString();
                label[i].TabIndex = i;

                this.Controls.Add(lampView[i]);
                this.Controls.Add(switchView[i]);
                this.Controls.Add(label[i]);
            }

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += TickEvent_Handler;
            timer.Enabled = true;
        }

        void SwitchChanged_Handler(Object sender, EventArgs e)
        {
            if (this.robotConsole != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (switchView[i] == sender)
                    {
                        //MessageBox.Show("Switch: " + ((SwitchEventArg)e).State);
                        robotConsole.Switches[i] = ((SwitchEventArg)e).State; 
                    }
                }

                // Kopieren wir Testhalber die Switches auf die LED's
                //robotConsole.Lamps.Data = robotConsole.Switches.Data;
            }
        }

        public RobotCtrl.Console RobotConsole
        {
            set{ this.robotConsole = value; }
        }

        void TickEvent_Handler(Object sender, EventArgs e)
        {
            if (this.robotConsole != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    switchView[i].State = this.robotConsole.Switches[i];
                    lampView[i].State = this.robotConsole.Lamps[i];
                }
            }
        }
    }
}
