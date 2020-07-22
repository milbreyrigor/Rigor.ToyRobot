using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Common
{
    public static class CommandNames
    {
        public static CommandName Place = new CommandName(
           new Guid("{D5E45E90-C677-4B6F-8D9E-2587C44939D0}"),
           "PLACE");

        public static CommandName Move = new CommandName(
           new Guid("{E34358EC-56B1-4D2A-A520-024996A8D629}"),
           "MOVE");

        public static CommandName Left = new CommandName(
           new Guid("{02E96C08-9440-45F3-B0FC-6002AE09C210}"),
           "LEFT");

        public static CommandName Right = new CommandName(
           new Guid("{2270FF07-A0D5-429F-848F-A647072B2E25}"),
           "RIGHT");

        public static CommandName Report = new CommandName(
           new Guid("{6275E70D-34C1-4227-AD12-202E7A7FCF58}"),
           "REPORT");

        public static IEnumerable<CommandName> GetAll()
        {

            var fields = typeof(CommandNames).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<CommandName>();

        }
    }
}
