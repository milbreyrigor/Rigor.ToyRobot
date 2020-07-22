using Rigor.ToyRobot.Common.Common;

namespace Rigor.ToyRobot.Common.Interfaces
{
    public interface IMoveStrategy
    {
        Direction Direction { get; }

        void Execute(IRobot robot);
    }
}
