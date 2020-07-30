using Rigor.ToyRobot.Console.Handlers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Console
{
    public static class ConsoleWriter
    {
        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="toSameLine">if set to <c>true</c> [to same line].</param>
        public static void WriteLine(string line, bool toSameLine = false)
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

                foreach (HandlerBase item in HandlerBaseFactory.GetAllHandlerBases())
                {
                    WriteLine(item.ToString());
                    WriteLine("");
                }
            }
            else
            {
                try
                {
                    HandlerBase c = HandlerBaseFactory.GetAllHandlerBases().Where(x=>x.CommandArgs == commandToShow).FirstOrDefault();

                    WriteLine(c?.ToString());

                    WriteLine("");
                }
                catch (KeyNotFoundException)
                {
                    WriteLine("Invalid command provided.");
                }
            }
        }
    }
}
