using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Data
{
    [Serializable]
    public class ChallengeConfiguration
    {
        public Guid ChallengeGuid { get; set; }

        public string ChallengeDetails { get; set; }
    }
}
