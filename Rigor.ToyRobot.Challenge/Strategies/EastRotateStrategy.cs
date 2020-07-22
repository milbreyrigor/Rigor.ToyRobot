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
    public class EastRotateStrategy : IRotateStrategy
    {
        public Direction Direction => Directions.East;

        public void Execute(IRobot robot, bool anticlockwise = false)
        {
            var currentPosition = robot.CurrentPosition;
            var newDirection = Directions.South;

            if (anticlockwise)
            {
                newDirection = Directions.North;
            }

            robot.SetPosition(new MatPosition(new MatLocation(currentPosition.Location.X, currentPosition.Location.Y), newDirection));
        }
    }
}
