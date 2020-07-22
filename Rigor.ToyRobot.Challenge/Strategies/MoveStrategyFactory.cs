using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Strategies
{
    public static class MoveStrategyFactory
    {
        public static IMoveStrategy CreateMoveStrategy(Direction direction)
        {
            IMoveStrategy strategy = GetMoveStrategies().FirstOrDefault(x => x.Direction.Equals(direction)); 
            return strategy;
        } 

        public static List<IMoveStrategy> GetMoveStrategies()
        {
            List<IMoveStrategy> result = new List<IMoveStrategy>();

            try
            {
                var type = typeof(IMoveStrategy);
                foreach (IMoveStrategy strategy in Assembly.GetExecutingAssembly().GetTypes().Where(c => type.IsAssignableFrom(c)).Select(c => Activator.CreateInstance(c)))
                {
                    result.Add(strategy);
                }

                result = result.ToList();
            }

            catch (Exception e)
            {

            }

            return result;
        }
    }
}
