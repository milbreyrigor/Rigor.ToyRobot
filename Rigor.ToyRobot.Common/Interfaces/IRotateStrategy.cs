using Rigor.ToyRobot.Common.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IRotateStrategy
    {
        Direction Direction { get; }

        void Execute(IRobot robot, bool anticlockwise=false);
    }
}
