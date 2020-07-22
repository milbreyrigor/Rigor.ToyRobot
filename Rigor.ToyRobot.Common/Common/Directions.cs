using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Common
{
    public static class Directions
    {
        public static Direction North = new Direction(
           new Guid("{29063BE3-898F-4461-9718-EEE935AD32AD}"),
           "NORTH",
           0);

        public static Direction South = new Direction(
           new Guid("{808A8075-795C-4701-88E8-7C55CAF08831}"),
           "SOUTH",
           1);

        public static Direction West = new Direction(
           new Guid("{001977B9-2BFE-4D83-8F0F-61FD024EE6EC}"),
           "WEST",
           2);

        public static Direction East = new Direction(
           new Guid("{23906E4C-129D-45AD-9886-2954B9471346}"),
           "EAST",
           3);

        public static IEnumerable<Direction> GetAll()
        {

            var fields = typeof(Directions).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<Direction>();

        }
    }
}
