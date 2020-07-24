using Rigor.ToyRobot.Common.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IRobot
    {
        MatPosition CurrentPosition { get; }

        IList<IMoveStrategy> MoveStrategies { get; }

        IList<IRotateStrategy> RotateStrategies { get; }

        void SetMat(IMat mat);

        void SetPosition(MatPosition position);

        void Move();

        void Left();

        void Right();

        string Report();
    }
}
