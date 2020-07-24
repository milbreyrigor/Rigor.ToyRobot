using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Commands
{
    public static class CommandFactory
    {
        public static IRobotCommand CreateCommand(string commandName)
        {
            IRobotCommand command = GetCommands().FirstOrDefault(x => x.Command.Name.Equals(commandName));
            return command;
        }

        public static List<IRobotCommand> GetCommands()
        {
            List<IRobotCommand> result = new List<IRobotCommand>();

            try
            {
                var type = typeof(IRobotCommand);
                foreach (IRobotCommand command in Assembly.GetExecutingAssembly().GetTypes().Where(c => type.IsAssignableFrom(c)).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(command);
                }
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}
