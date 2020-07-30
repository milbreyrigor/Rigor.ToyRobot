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
        
    }
}
