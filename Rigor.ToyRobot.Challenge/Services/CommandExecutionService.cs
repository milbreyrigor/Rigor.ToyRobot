using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    public class CommandExecutionService: ServicesBase
    {
        public event EventHandler<string> CommandExecuted;

        public void ExecuteCommands(IRobot robot, List<IRobotCommand> commands)
        {
            foreach (IRobotCommand command in commands)
            {
                string message = command.Execute(robot);
                OnCommandExecuted(this, message);
            }
        }

        private void OnCommandExecuted(object sender, string message)
        {
            CommandExecuted?.Invoke(sender, message);
        }

    }
}
