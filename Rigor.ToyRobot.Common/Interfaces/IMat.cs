using Rigor.ToyRobot.Common.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IMat
    {
        uint Height { get; }

        uint Width { get; }

        bool IsValidPosition(MatLocation position);
    }
}
