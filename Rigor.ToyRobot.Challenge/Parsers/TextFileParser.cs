using Rigor.ToyRobot.Challenge.Commands;
using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Parsers
{
    public class TextFileParser : ICommandFileParser
    {
        public string FileExtension => ".txt";

        public List<List<IRobotCommand>> ParseFile(string filePath)
        {
            List<List<IRobotCommand>> commands = new List<List<IRobotCommand>>();

            try
            {
                List<string> listOfCommands = GetCommandLinesFromFile(filePath);

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

                        if(placeCommand !=null)
                        {
                            commandGroup = new List<IRobotCommand>();
                            commandGroup.Add(placeCommand);
                            continue;
                        }
                        
                    }

                    if(commandGroup != null)
                    {
                        if (!command.StartsWith(CommandNames.Place.Name))
                        {
                            IRobotCommand robotCommand = CommandFactory.CreateCommand(command.ToUpper());
                            commandGroup.Add(robotCommand);
                        }

                        if(counter == listOfCommands.Count)
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

        public List<string> GetCommandLinesFromFile(string filePath)
        {
            List<string> commands = new List<string>();

            try
            {
                commands = File.ReadLines(filePath)
                    .SkipWhile(line => !line.StartsWith(CommandNames.Place.Name))
                    .Where(line => !String.IsNullOrEmpty(line))
                    .ToList();
            }
            catch
            {

            }

            return commands;
        }


        public IRobotCommand GetPlaceRobotCommand(string command)
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
