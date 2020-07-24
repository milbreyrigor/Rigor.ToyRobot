using Newtonsoft.Json;

using Rigor.ToyRobot.Challenge.Challenges;
using Rigor.ToyRobot.Challenge.Components;
using Rigor.ToyRobot.Challenge.Parsers;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console.Handlers
{
    public class RunByConfigurationHandler: HandlerBase
    {
        private string _errMsg;

        private string _configurationFilePath;

        private string _inputCommandFilePath;

        private static string CONFIG_FILE_SWITCH = "C";

        private static string INPUT_FILE_SWITCH = "I";

        private ChallengeConfiguration _challengeConfiguration;

        private IChallenge _challenge;

        public RunByConfigurationHandler() : base()
        {



        }

        public override string CommandArgs
        {
            get
            {
                return "-RunConfiguration";
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
                var description = "Runs the Toy Robot Challenge using a pre-defined configuration. \n" +
                    "Please ensure configuration settings file is available and set.\n";

                var options = $"[/{CONFIG_FILE_SWITCH}]\t\tProvide the full path to the configuration file\n";

                options += "----\n";
                options += $"[/{INPUT_FILE_SWITCH}]\t\tProvide the full path to the input commands file\n";
                options += $"Example: -RunConfiguration /{CONFIG_FILE_SWITCH} {@"C:\temp\SquareMatChallengeConfig.config"} /{INPUT_FILE_SWITCH} {@"C:\temp\InputCommands.txt"}";

                return description + options;
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
                return new Guid("{942416D1-5A34-41B3-A021-FB31C24ABF84}");
            }
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public override void ExecuteCommand()
        {
            try
            {
                ProcessParams();
                ProcessFile();

            }
            catch (Exception e)
            {
                _errMsg = e.Message;


                throw e;
            }
        }


        #region Private functions

        /// <summary>
        /// Processes the Video configuration XML file.
        /// </summary>        
        private void ProcessFile()
        {
            try
            {
                if(_challengeConfiguration != null)
                {
                    _challenge = ChallengeFactory.CreateChallenge(_challengeConfiguration.ChallengeGuid);

                    List<IRobot> robots = new List<IRobot>();
                    foreach(RobotConfiguration robotConfiguration in _challengeConfiguration.Robots)
                    {
                        if(!robots.Any(x=> (x as IHaveIdentifier).Name == robotConfiguration.Name))
                        {
                            robots.Add(new Robot(robotConfiguration.Name, robotConfiguration.Guid));
                        }
                    }

                    _challenge.Initialize(robots, _challengeConfiguration.ChallengeMatDetails, _challengeConfiguration.IsSinglePlayer);
                    _challenge.CommandExecuted += _challenge_CommandExecuted;
                    SetMessage(this, new EventArgs.HandlerEventArgs(this, "Toy Robot Challenge initialized...", false));

                    if(!String.IsNullOrEmpty(_inputCommandFilePath))
                    {
                        FileInfo fileInfo = new FileInfo(_inputCommandFilePath);

                        if(fileInfo.Exists)
                        {
                            ICommandFileParser parser = CommandFileParserFactory.CreateParser(fileInfo.Extension);
                            List<List<IRobotCommand>> robotCommands = parser.ParseFile(_inputCommandFilePath);
                            foreach(List<IRobotCommand> commandlist in robotCommands)
                            {
                                _challenge.ExecuteCommands(commandlist);
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                _errMsg = String.Format("Unable to process the configuration due to an error. {0}", ex.Message);
                throw new Exception(_errMsg);

            }
        }

        private void _challenge_CommandExecuted(object sender, string e)
        {
            SetMessage(this, new EventArgs.HandlerEventArgs(this, e, false));

        }





        /// <summary>
        /// Processes the parameters.
        /// </summary>
        /// <exception cref="System.Exception">
        /// </exception>
        private void ProcessParams()
        {
            try
            {

                _configurationFilePath = "";

                bool pass = true;
                if (CommandParams == null)
                {

                    pass = false;
                }
                else if (CommandParams.Length < 1)
                {
                    pass = false;
                }

                if (!pass)
                {
                    _errMsg = "Invalid parameters applied to this command.";

                    throw new Exception(_errMsg);
                }

                string commandParams = "";
                for (int i = 0; i < CommandParams.Length; i++)
                {
                    commandParams += CommandParams[i] + " ";
                }


                string[] commandParamsArray = commandParams.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < commandParamsArray.Length; i++)
                {
                    try
                    {

                        if (commandParamsArray[i].ToUpper().StartsWith(CONFIG_FILE_SWITCH) && commandParamsArray[i].ToString().ElementAt(1) == ' ')
                        {
                            _configurationFilePath = commandParamsArray[i].Remove(0, CONFIG_FILE_SWITCH.Length);


                            FileInfo sFileInfo = new FileInfo(_configurationFilePath);

                            if (!sFileInfo.Exists)
                            {
                                _errMsg = "Invalid configuration file name provided: " + _configurationFilePath;

                                throw new Exception(_errMsg);
                            }
                        }

                        if (commandParamsArray[i].ToUpper().StartsWith(INPUT_FILE_SWITCH) && commandParamsArray[i].ToString().ElementAt(1) == ' ')
                        {
                            _inputCommandFilePath = commandParamsArray[i].Remove(0, INPUT_FILE_SWITCH.Length);


                            FileInfo sFileInfo = new FileInfo(_inputCommandFilePath);

                            if (!sFileInfo.Exists)
                            {
                                _errMsg = "Invalid input commmands file name provided: " + _configurationFilePath;

                                throw new Exception(_errMsg);
                            }
                        }
                    }
                    catch (Exception d)
                    {
                        _errMsg = "Invalid parameters provided.";

                        throw d;
                    }
                }

                SetMessage(this, new EventArgs.HandlerEventArgs(this, "Checking config file...", false));


                _challengeConfiguration = JsonConvert.DeserializeObject<ChallengeConfiguration>(File.ReadAllText(_configurationFilePath));

                if(_challengeConfiguration == null)
                {
                    throw new Exception("Unable to parse the given configuration file due to an error");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~RunByConfigurationHandler()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }


        #endregion

        #endregion

    }
}
