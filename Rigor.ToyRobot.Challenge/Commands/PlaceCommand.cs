using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Commands
{
    public class PlaceCommand : IRobotCommand
    {
        public CommandName Command => CommandNames.Place;

        public MatPosition Position { get; set; }

        public string Execute(IRobot robot)
        {
            string result = String.Empty;

            try
            {
                robot.SetPosition(Position);
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
