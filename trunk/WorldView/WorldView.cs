using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldView
{
    public class WorldView : UserControl
    {
        public WorldView()
        {
            InitializeComponent();

        }

        public int xMin{set { xMin = value;}}
        int xMin;

        public int yMin { set { yMin = value; } }
        int yMin;

        public int xMax { set { xMax = value; } }
        int xMax;

        public int yMax { set { yMax = value; } }
        int yMax;

        public int gridRaster_in_Meter { set { gridRaster_in_Meter = value; } }
        int gridRaster_in_Meter = 1;
    }
}
