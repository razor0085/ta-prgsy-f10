using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RobotView;

namespace Test_WorldView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WorldView worldView = new WorldView();
            this.Controls.Add(worldView);
        }
    }
}