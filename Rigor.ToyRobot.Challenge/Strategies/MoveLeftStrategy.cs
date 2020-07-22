using Rigor.ToyRobot.Challenge.Components;
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
    public class MoveLeftStrategy : IMoveStrategy
    {
        public Direction Direction => Directions.West;

        public void Execute(IRobot robot)
        {
            var currentPosition = robot.CurrentPosition;
            robot.SetPosition(new MatPosition(new MatLocation(currentPosition.Location.X - 1, currentPosition.Location.Y), currentPosition.Direction));

        }
    }
}
