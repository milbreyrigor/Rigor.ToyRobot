using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Components
{
    public static class RobotFactory
    {
        public static List<IRobot> CreateRobots(List<RobotConfiguration> robotConfigurations)
        {
            List<IRobot> robots = new List<IRobot>();
            foreach (RobotConfiguration robotConfiguration in robotConfigurations)
            {
                if (!robots.Any(x => (x as IHaveIdentifier).Name == robotConfiguration.Name))
                {
                    robots.Add(new Robot(robotConfiguration.Name, robotConfiguration.Guid));
                }
            }

            return robots;
        }
    }
}
