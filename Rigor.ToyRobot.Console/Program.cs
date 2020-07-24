using Newtonsoft.Json;

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

                _availableCommands = HandlerBase.GetAvailableCommands();


                foreach (KeyValuePair<string, HandlerBase> c in _availableCommands.ToList())
                {
                    c.Value.ReportMessageSet += Value_ReportMessage;
                }

                string command = "";
                if (newArgs.Count < 1)
                {
                    PrintHelp();
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
                        WriteLine($"Error: Incompatible command received. Please try again. {keyNotFoundException.Message}");
                    }
                    catch (Exception e)
                    {
                        WriteLine("An error occured in executing the function.\n" + _availableCommands[command].GetErrorMessage);
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
            WriteLine(e.Message, e.WriteToSameLine);
        }

        /// <summary>
        /// Prints the help.
        /// </summary>
        internal static void PrintHelp(string commandToShow = null)
        {
            if (commandToShow == null)
            {
                System.Console.ForegroundColor = ConsoleColor.White;

                var name = Assembly.GetAssembly(typeof(HandlerBase)).GetName().Name.ToString();

                WriteLine(String.Format("----{0}----", name));

                WriteLine("");

                WriteLine("Usage: (Case sensitive)");

                WriteLine("");

                System.Console.ResetColor();

                foreach (KeyValuePair<string, HandlerBase> item in _availableCommands)
                {
                    HandlerBase c = item.Value;

                    WriteLine(c.ToString());
                    WriteLine("");
                }
            }
            else
            {
                try
                {
                    HandlerBase c = _availableCommands[commandToShow];

                    WriteLine(c.ToString());

                    WriteLine("");
                }
                catch (KeyNotFoundException)
                {
                    WriteLine("Invalid command provided.");
                }
            }
        }


        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="toSameLine">if set to <c>true</c> [to same line].</param>
        private static void WriteLine(string line, bool toSameLine = false)
        {
            if (toSameLine)
            {
                System.Console.Write("\r{0}", line);
            }
            else
            {

                System.Console.WriteLine(line);
            }

        }
    }
}
