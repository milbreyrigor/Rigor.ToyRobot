using Newtonsoft.Json;

using Rigor.ToyRobot.Challenge.Services;
using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Console.EventArgs;
using Rigor.ToyRobot.Console.Handlers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rigor.ToyRobot.Console
{
    class Program
    {
        private static Dictionary<string, HandlerBase> _availableCommands;

        private static string EXIT_COMMAND = "EXIT";

        static void Main(string[] args)
        {
            try
            {

                List<string> newArgs = args.ToList();

                InitializeServices();

                _availableCommands = GetAvailableCommands();


                foreach (KeyValuePair<string, HandlerBase> c in _availableCommands.ToList())
                {
                    c.Value.ReportMessageSet += Value_ReportMessage;
                }

                string command = "";
                if (newArgs.Count < 1)
                {
                    ConsoleWriter.PrintHelp();
                }
                else
                {
                    command = newArgs[0];

                    List<string> lstParams = new List<string>();

                    if (newArgs.Count > 1)
                    {
                        string strParams = "";
                        for (int i = 1; i < newArgs.Count; i++)
                        {
                            lstParams.Add(newArgs[i]);

                            strParams += newArgs[i] + "; ";
                        }
                   }

                    try
                    {
                        if (command != "" & command != EXIT_COMMAND)
                        {
                            _availableCommands[command].CommandParams = lstParams.ToArray();
                            _availableCommands[command].ExecuteCommand();
                            _availableCommands[command].Dispose();
                        }

                    }
                    catch (KeyNotFoundException keyNotFoundException)
                    {
                        ConsoleWriter.WriteLine($"Error: Incompatible command received. Please try again. {keyNotFoundException.Message}");
                    }
                    catch (Exception e)
                    {
                        ConsoleWriter.WriteLine("An error occured in executing the function.\n" + _availableCommands[command].GetErrorMessage);
                    }

                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("An error occured processing the request.");

                System.Console.WriteLine("Press any key to quit.");


            }

            System.Console.ReadKey();

        }

        private static void Value_ReportMessage(object sender, HandlerEventArgs e)
        {
            ConsoleWriter.WriteLine(e.Message, e.WriteToSameLine);
        }

        
        internal static Dictionary<string, HandlerBase> GetAvailableCommands()
        {
            List<HandlerBase> cmds = new List<HandlerBase>();

            Dictionary<string, HandlerBase> result = new Dictionary<string, HandlerBase>();

            try
            {
                cmds = HandlerBaseFactory.GetAllHandlerBases();

                foreach (HandlerBase c in cmds)
                {
                    result.Add(c.CommandArgs, c);
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        internal static void InitializeServices()
        {
            try
            {
                var allGeneralServices = ServicesFactory.CreateAllServices()?.ToList();
                allGeneralServices?.ForEach(x =>
                {
                    SharedServices.SetService(x, x.GetType());
                });
            }

            catch (Exception e)
            {
            }
        }

    }
}
