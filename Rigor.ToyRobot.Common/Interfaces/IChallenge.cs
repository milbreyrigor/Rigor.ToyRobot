using System;
using System.Collections.Generic;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IChallenge
    {
        IList<IRobot> Robots { get; }

        IRobot ActiveRobot { get; }

        bool IsSinglePlayer { get; }

        IMat Mat { get; }

        void Initialize(IList<IRobot> robots, string matDetails, bool isSinglePlayer = true);

        void SetActiveRobot(Guid guid);

    }
}
