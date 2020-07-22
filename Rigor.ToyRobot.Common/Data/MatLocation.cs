using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Data
{
    public class MatLocation
    {
        public uint X { get; set; }

        public uint Y { get; set; }

        public MatLocation()
        {
            this.X = 0;
            this.Y = 0;
        }

        public MatLocation(uint x, uint y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
