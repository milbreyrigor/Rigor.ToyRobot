using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface ICommandFileParser
    {
        string FileExtension { get; }

        List<List<IRobotCommand>> ParseFile(string filePath);
    }
}
