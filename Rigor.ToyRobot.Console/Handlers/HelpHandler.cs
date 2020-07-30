using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.Handlers
{
    public class HelpHandler : HandlerBase
    {
        #region Fields

        private string _errMsg;

        private bool _showAll;

        private string _commandToShow;

        private static string SHOW_COMMAND_HELP_SWITCH = "N";

        #endregion

        public HelpHandler() : base()
        {

        }

        /// <summary>
        /// Gets the command arguments.
        /// </summary>
        /// <value>
        /// The command arguments.
        /// </value>
        public override string CommandArgs
        {
            get
            {
                return "-GetHelp";
            }
        }

        /// <summary>
        /// Gets the command arguments description.
        /// </summary>
        /// <value>
        /// The command arguments description.
        /// </value>
        public override string CommandArgsDescription
        {
            get
            {
                var res = "Provides more information about available commands.\n";

                var options = $"[/{SHOW_COMMAND_HELP_SWITCH}]\t\tProvide information only about this command. (optional)\n";

                options += "----\n";
                options += $"Example: -GetHelp /{SHOW_COMMAND_HELP_SWITCH} -RunByConfiguration";

                return res + options;
            }
        }

        /// <summary>
        /// Gets the get error message.
        /// </summary>
        /// <value>
        /// The get error message.
        /// </value>
        public override string GetErrorMessage
        {
            get
            {
                return _errMsg;
            }
        }

        /// <summary>
        /// Gets the handler unique identifier.
        /// </summary>
        /// <value>
        /// The handler unique identifier.
        /// </value>
        public override Guid HandlerGuid
        {
            get
            {
                return new Guid("{3A86491C-FD8C-4009-A53C-6E3E86FE00CF}");
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <exception cref="System.Exception">Invalid Parameter found.</exception>
        public override void ExecuteCommand()
        {
            _showAll = false;
            if (CommandParams == null)
            {
                _showAll = true;
            }
            else if (CommandParams.Length < 1) { _showAll = true; }


            if (_showAll)
            {
                ConsoleWriter.PrintHelp();


                return;
            }

            string str = "";
            for (int i = 0; i < CommandParams.Length; i++)
            {
                str += CommandParams[i] + " ";
            }

            string[] spl = str.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            bool isParamOk = false;

            try
            {
                for (int i = 0; i < spl.Length; i++)
                {
                    if (spl[i].ToUpper().StartsWith(SHOW_COMMAND_HELP_SWITCH) && spl[i].ToString().ElementAt(1) == ' ')
                    {
                        _commandToShow = spl[i].Remove(0, SHOW_COMMAND_HELP_SWITCH.Length).Trim();

                        ConsoleWriter.PrintHelp(_commandToShow);

                        isParamOk = true;
                    }
                }

                if (!isParamOk)
                    throw new Exception("Invalid Parameter found.");

            }
            catch (Exception d)
            {
                _errMsg = "Invalid parameters provided.";

                throw d;
            }
        }
    }
}
