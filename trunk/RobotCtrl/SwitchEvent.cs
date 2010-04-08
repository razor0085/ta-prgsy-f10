using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class SwitchEvent : System.EventArgs
    {
        int number;

        public SwitchEvent(int switchNumber)
        {
            this.number = switchNumber;
        }

        public int getSwitchNumber()
        {
            return number;
        }
    }
}
