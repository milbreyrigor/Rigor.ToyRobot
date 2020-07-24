using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Commands
{
    public class LeftCommand : IRobotCommand
    {
        public CommandName Command => CommandNames.Left;

        public string Execute(IRobot robot)
        {
            string result = String.Empty;

            try
            {
                robot.Left();
                result = $"{Command.Name} Success";
            }
            catch
            {
                result = "Fail";
            }

            return result;
        }
    }
}
