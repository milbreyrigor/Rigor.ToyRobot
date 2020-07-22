using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Common
{
    [Serializable()]
    public class Direction : Enumeration
    {
        public int Value { get; }

        public Direction() : base(Guid.Empty, String.Empty)
        {

        }


        public Direction(Guid guid, string name, int value) : base(guid, name)
        {
            Value = value;
        }
    }
}
