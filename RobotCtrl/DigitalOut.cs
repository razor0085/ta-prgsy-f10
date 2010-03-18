using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace RobotCtrl
{
    /**
     * @brief DigitalOut, damit der Roboter LED's setzen kann
     */
    public class DigitalOut
    {
        /**
         * Property Data Setzen und lesen von 4 Bit
         */
        public virtual int Data
        {
            get { return data; }
            set { data = value; }
        }

        protected int data;

        /**
         * Indexer Setzen und lesen eines bestimmten Bits
         * 
         * @param index int Indexnummer
         * @return bool Ist Bit gesetzt [TRUE/FALSE]
         */
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
