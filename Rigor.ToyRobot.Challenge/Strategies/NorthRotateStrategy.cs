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
    public class NorthRotateStrategy : IRotateStrategy
    {
        public Direction Direction => Directions.North;

        public void Execute(IRobot robot, bool anticlockwise = false)
        {
            var currentPosition = robot.CurrentPosition;
            var newDirection = Directions.East;

            if(anticlockwise)
            {
                newDirection = Directions.West;
            }

            robot.SetPosition(new MatPosition(new MatLocation(currentPosition.Location.X, currentPosition.Location.Y), newDirection));
        }
    }
}
