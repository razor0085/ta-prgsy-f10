using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RobotView;
using RobotCtrl;
using System.Threading;

namespace Test_LampView
{
    public partial class Test_LampView_Form : Form
    {
        public event System.EventHandler DigitalInChanged;
        public event System.EventHandler DigitalOutChanged;

        Thread digitalIn;
        Thread digitalOut;

        LampView[] lampView = new LampView[4];
        SwitchView[] switchView = new SwitchView[4];
        Label[] label = new Label[4];
        RobotCtrl.Console robotConsole = new RobotCtrl.Console(0);
        bool running = true;

        public Test_LampView_Form()
        {
            InitializeComponent();
            
            DigitalOutChanged += DigitalOutChanged_Handler;
            DigitalInChanged += DigitalInChanged_Handler;

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
            digitalIn = new Thread(checkDigitalIn);
            digitalIn.Name = "checkDigitalIn";
            digitalIn.Start();
            digitalOut = new Thread(checkDigitalOut);
            digitalOut.Name = "checkDigitalOut";
            digitalOut.Start();

            this.Closing += new CancelEventHandler(Form_Closing);
        }

        void checkDigitalIn()
        {
            int data = 0;
            while (this.running)
            {
                //MessageBox.Show("checkDigitalIn()");
                if (data != robotConsole.Switches.Data)
                {
                    data = robotConsole.Switches.Data;
                    DigitalInChanged(robotConsole.Switches, new DigitalInEventArg(-1));
                }
                Thread.Sleep(1000);
            }
        }

        void checkDigitalOut()
        {
            int data = 0;
            while (this.running)
            {
                if (data != robotConsole.Lamps.Data)
                {
                    data = robotConsole.Lamps.Data;
                    DigitalOutChanged(robotConsole.Lamps, new DigitalOutEventArg(-1));
                }
                Thread.Sleep(1000);
            }
        }

        void DigitalOutChanged_Handler(Object sender, EventArgs e)
        {
            if (((DigitalOutEventArg)e).BitNumber == -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    lampView[i].State = ((DigitalOut)sender)[i];
                }
            }
            else
            {
                lampView[((DigitalOutEventArg)e).BitNumber].State = ((DigitalOut)sender)[((DigitalOutEventArg)e).BitNumber];
            }
        }

        void DigitalInChanged_Handler(Object sender, EventArgs e)
        {
            if (((DigitalInEventArg)e).BitNumber == -1)
            {
                for (int i = 0; i < 4; i++)
                {
                    switchView[i].State = ((DigitalIn)sender)[i];
                }
            }
            else
            {
                switchView[((DigitalInEventArg)e).BitNumber].State = ((DigitalIn)sender)[((DigitalInEventArg)e).BitNumber];
            }
        }

        void SwitchChanged_Handler(Object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (switchView[i] == sender)
                {
                    robotConsole.Switches[i] = ((SwitchEventArg)e).State;
                }
            }

            // Kopieren wir Testhalber die Switches auf die LED's
            robotConsole.Lamps.Data = robotConsole.Switches.Data;
        }

        public void Form_Closing(object sender, CancelEventArgs cArgs)
        {
            if (sender == this)
            {
                this.running = false;
            }
        }
    }

    public class DigitalInEventArg : EventArgs
    {
        public int BitNumber
        {
            get { return bitNumber; }
        }

        int bitNumber;

        public DigitalInEventArg(int bitNumber)
        {
            this.bitNumber = bitNumber;
        }
    }

    public class DigitalOutEventArg : EventArgs
    {
        public int BitNumber
        {
            get { return bitNumber; }
        }

        int bitNumber;

        public DigitalOutEventArg(int bitNumber)
        {
            this.bitNumber = bitNumber;
        }
    }
}
