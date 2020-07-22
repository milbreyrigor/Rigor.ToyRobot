using Rigor.ToyRobot.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Data
{
    public class MatPosition
    {
        public MatLocation Location { get; set; }

        public Direction Direction { get; set; }

        public MatPosition()
        {
            this.Location = new MatLocation();
            this.Direction = Directions.North;
        }

        public MatPosition(MatLocation location, int directionValue)
        {
            this.Location = location;
            var dir = Directions.GetAll().Where(x=>x.Value == directionValue).FirstOrDefault();

            if(dir != null)
            {
                this.Direction = dir;
            }

        }

        public MatPosition(MatLocation location, Direction direction)
        {
            this.Location = location;
            this.Direction = direction;
        }

    }
}
