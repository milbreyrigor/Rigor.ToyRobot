using Rigor.ToyRobot.Common.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IRobotCommand
    {
        CommandName Command { get; }

        string Execute(IRobot robot);
    }
}
