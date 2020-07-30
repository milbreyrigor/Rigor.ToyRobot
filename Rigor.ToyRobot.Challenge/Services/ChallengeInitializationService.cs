using Rigor.ToyRobot.Challenge.Challenges;
using Rigor.ToyRobot.Challenge.Components;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Services
{
    public class ChallengeInitializationService: ServicesBase
    {
        public IChallenge InitializeChallenge(ChallengeConfiguration configuration)
        {
            IChallenge challenge = ChallengeFactory.CreateChallenge(configuration.ChallengeGuid);

            if (challenge != null)
            {
                List<IRobot> robots = RobotFactory.CreateRobots(configuration.Robots);
                challenge.Initialize(robots, configuration.ChallengeMatDetails, configuration.IsSinglePlayer);
            }

            return challenge;
        }
    }
}
