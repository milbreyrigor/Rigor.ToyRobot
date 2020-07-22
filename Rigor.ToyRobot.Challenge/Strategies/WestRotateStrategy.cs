using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Strategies
{
    public class WestRotateStrategy : IRotateStrategy
    {
        public Direction Direction => Directions.West;

        public void Execute(IRobot robot, bool anticlockwise = false)
        {
            var currentPosition = robot.CurrentPosition;
            var newDirection = Directions.North;

            if (anticlockwise)
            {
                newDirection = Directions.South;
            }

            robot.SetPosition(new MatPosition(new MatLocation(currentPosition.Location.X, currentPosition.Location.Y), newDirection));
        }
    }
}
