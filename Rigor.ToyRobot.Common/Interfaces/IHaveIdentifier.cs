using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IHaveIdentifier
    {
        string Name { get; }

        Guid Guid { get; }

    }
}
