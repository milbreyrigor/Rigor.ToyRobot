using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Parsers
{
    public static class CommandFileParserFactory
    {
        public static ICommandFileParser CreateParser(string extension)
        {
            ICommandFileParser parser = GetParsers().FirstOrDefault(x => x.FileExtension.Equals(extension.ToLower()));
            return parser;
        }

        public static List<ICommandFileParser> GetParsers()
        {
            List<ICommandFileParser> result = new List<ICommandFileParser>();

            try
            {
                var type = typeof(ICommandFileParser);
                foreach (ICommandFileParser parser in Assembly.GetExecutingAssembly().GetTypes().Where(c => type.IsAssignableFrom(c)).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(parser);
                }
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}
