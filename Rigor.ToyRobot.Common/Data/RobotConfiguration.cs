using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Data
{
    [Serializable]
    public class RobotConfiguration
    {
        public string Name { get; set; }
        
        public Guid Guid { get; set; }
    }
}
