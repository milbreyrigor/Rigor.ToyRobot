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
    public static class RotateStrategyFactory
    {
        public static IRotateStrategy CreateRotateStrategy(Direction direction)
        {
            IRotateStrategy strategy = GetRotateStrategies().FirstOrDefault(x => x.Direction.Equals(direction)); 
            return strategy;
        } 

        public static List<IRotateStrategy> GetRotateStrategies()
        {
            List<IRotateStrategy> result = new List<IRotateStrategy>();

            try
            {
                var type = typeof(IRotateStrategy);
                foreach (IRotateStrategy strategy in Assembly.GetExecutingAssembly().GetTypes().Where(c => type.IsAssignableFrom(c)).Select(c => Activator.CreateInstance(c)))
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
