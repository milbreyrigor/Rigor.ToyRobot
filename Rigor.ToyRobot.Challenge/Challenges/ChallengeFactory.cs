using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Challenges
{
    public static class ChallengeFactory
    {
        public static IChallenge CreateRotateStrategy(Guid guid)
        {
            IChallenge challenge = GetChallenges().FirstOrDefault(x => (x as IHaveIdentifier).Guid == guid);
            return challenge;
        }

        public static List<IChallenge> GetChallenges()
        {
            List<IChallenge> result = new List<IChallenge>();

            try
            {
                var type = typeof(IChallenge);
                foreach (IChallenge strategy in Assembly.GetExecutingAssembly().GetTypes().Where(c => type.IsAssignableFrom(c)).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(strategy);
                }
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}
