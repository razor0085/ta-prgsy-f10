using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    public class DigitalIn
    {
        public virtual int Data
        {
            get { return data; }
            set { data = value; }
        }
        
        int data;

        public virtual bool this[int index]
        {
            get
            {
                if (index < 4 && index > -1)
                {
                    return (data & (1 << index)) > 0 ? true : false;
                }
                return false;
            }
            set
            {
                if (index < 4 && index > -1)
                {
                    if (value == true)
                    {
                        // bit setzen
                        data |= (1 << index);
                    }
                    else
                    {
                        // bit löschen
                        data &= (~(1 << index) & 0xf);
                    }
                }
            }
        }
    }
}
