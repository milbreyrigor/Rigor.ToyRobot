using Rigor.ToyRobot.Challenge.Commands;
using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    public class CommandConversionService: ServicesBase
    {
        public List<List<IRobotCommand>> ConvertStringCommands(List<string> listOfCommands)
        {
            List<List<IRobotCommand>> commands = new List<List<IRobotCommand>>();

            try
            {
                List<IRobotCommand> commandGroup = null;
                int counter = 0;
                foreach (string command in listOfCommands)
                {
                    counter++;

                    if (command.StartsWith(CommandNames.Place.Name))
                    {
                        if (commandGroup != null)
                        {
                            commands.Add(commandGroup);
                            commandGroup = null;
                        }

                        IRobotCommand placeCommand = GetPlaceRobotCommand(command);

                        if (placeCommand != null)
                        {
                            commandGroup = new List<IRobotCommand>();
                            commandGroup.Add(placeCommand);
                            continue;
                        }

                    }

                    if (commandGroup != null)
                    {
                        if (!command.StartsWith(CommandNames.Place.Name))
                        {
                            IRobotCommand robotCommand = CommandFactory.CreateCommand(command.ToUpper());
                            commandGroup.Add(robotCommand);
                        }

                        if (counter == listOfCommands.Count)
                        {
                            commands.Add(commandGroup);
                        }
                    }


                }
            }
            catch
            {

            }

            return commands;
        }

        private IRobotCommand GetPlaceRobotCommand(string command)
        {
            IRobotCommand robotCommand = null;

            try
            {
                string[] splitCmd = command.Split(' ');

                if (splitCmd != null && splitCmd.Length == 2)
                {
                    string[] splitParams = splitCmd[1].Split(',');

                    if (splitParams != null && splitParams.Length == 3)
                    {
                        uint x = 0;
                        uint y = 0;

                        if (uint.TryParse(splitParams[0], out x) && uint.TryParse(splitParams[1], out y))
                        {
                            Direction direction = Directions.GetAll().Where(d => d.Name == splitParams[2].ToUpper()).FirstOrDefault();

                            if (direction != null)
                            {
                                robotCommand = new PlaceCommand()
                                {
                                    Position = new Common.Data.MatPosition(new Common.Data.MatLocation(x, y), direction)
                                };
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return robotCommand;
        }
    }
}
