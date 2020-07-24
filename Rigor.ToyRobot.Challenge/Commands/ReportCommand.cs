using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Commands
{
    public class ReportCommand : IRobotCommand
    {
        public CommandName Command => CommandNames.Report;

        public string Execute(IRobot robot)
        {
            return robot.Report();
        }
    }
}
