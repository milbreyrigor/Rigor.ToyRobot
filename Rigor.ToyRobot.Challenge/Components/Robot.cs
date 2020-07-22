using Rigor.ToyRobot.Challenge.Strategies;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Components
{
    public class Robot : IRobot, IHaveIdentifier
    {
        private MatPosition currentPosition;

        private List<IMoveStrategy> moveStrategies;
        private List<IRotateStrategy> rotateStrategies;
        private Guid guid;
        private string name;
        private readonly IMat mat;

        public MatPosition CurrentPosition
        {
            get
            {
                return currentPosition;

            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public Guid Guid
        {
            get
            {
                return guid;
            }
        }



        public IList<IMoveStrategy> MoveStrategies
        {
            get
            {
                return moveStrategies;
            }
        }

        public IList<IRotateStrategy> RotateStrategies
        {
            get
            {
                return rotateStrategies;
            }
        }

        public Robot(IMat mat, string name, Guid guid)
        {
            moveStrategies = MoveStrategyFactory.GetMoveStrategies();
            rotateStrategies = RotateStrategyFactory.GetRotateStrategies();

            this.name = name;
            this.guid = guid;
            this.mat = mat;
            this.currentPosition = new MatPosition();
        }

        public void SetPosition(MatPosition position)
        {
            if (mat.IsValidPosition(position.Location))
            {
                this.currentPosition = position;
            }
        }

        public void Move()
        {
            IMoveStrategy strategy = MoveStrategies.Where(x => x.Direction == CurrentPosition.Direction).FirstOrDefault();

            if(strategy != null)
            {
                strategy.Execute(this);
            }
        }

        public void Left()
        {
            Rotate(true);
        }

        public void Right()
        {
            Rotate();
        }

        private void Rotate(bool anticlockwise=false)
        {
            IRotateStrategy strategy = RotateStrategies.Where(x => x.Direction == CurrentPosition.Direction).FirstOrDefault();

            if (strategy != null)
            {
                strategy.Execute(this, anticlockwise);
            }
        }
    }
}
