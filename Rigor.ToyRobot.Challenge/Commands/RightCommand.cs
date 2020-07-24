using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Commands
{
    public class RightCommand : IRobotCommand
    {
        public CommandName Command => CommandNames.Right;

        public string Execute(IRobot robot)
        {
            string result = String.Empty;

            try
            {
                robot.Right();
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
