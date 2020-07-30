using Rigor.ToyRobot.Challenge.Parsers;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    public class CommandParsingService: ServicesBase
    {
        public List<string> ParseCommandFile(string filepath)
        {
            List<string> commands = new List<string>();

            FileInfo fileInfo = new FileInfo(filepath);

            if (fileInfo.Exists)
            {
                ICommandFileParser parser = CommandFileParserFactory.CreateParser(fileInfo.Extension);
                commands = parser.GetCommandLinesFromFile(filepath);
            }

            return commands;
        }
    }
}
