using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Common
{
    [Serializable()]
    public class CommandName : Enumeration
    {

        public CommandName() : base(Guid.Empty, String.Empty)
        {

        }


        public CommandName(Guid guid, string name) : base(guid, name)
        {
        }
    }
}
