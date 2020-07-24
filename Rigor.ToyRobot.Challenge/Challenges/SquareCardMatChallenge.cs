using Newtonsoft.Json;

using Rigor.ToyRobot.Challenge.Components;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Challenges
{
    public class SquareCardMatChallenge : IChallenge, IHaveIdentifier
    {
        private IList<IRobot> robots;
        private IMat mat;
        private bool isSinglePlayer;
        private IRobot activeRobot;

        public event EventHandler<string> CommandExecuted;

        public IList<IRobot> Robots
        {
            get
            {
                return robots;
            }
        }

        public IMat Mat
        {
            get
            {
                return mat;
            }
        }

        public string Name => "Square Card Challenge";

        public Guid Guid => new Guid("{62DC51E0-50FD-43E4-A8D0-1A736F64FD12}");

        public bool IsSinglePlayer
        {
            get
            {
                return isSinglePlayer;
            }
        }

        public IRobot ActiveRobot
        {
            get
            {
                return activeRobot;
            }
        }

        public SquareCardMatChallenge()
        {
        }

        public void Initialize(IList<IRobot> robots, string matDetails, bool isSinglePlayer= true)
        {
            if(!String.IsNullOrEmpty(matDetails))
            {
                SquareMatConfiguration squareMatConfiguration = JsonConvert.DeserializeObject<SquareMatConfiguration>(matDetails);

                if(squareMatConfiguration != null)
                {
                    mat = new SquareCardMat(squareMatConfiguration.Width);
                }
            }

            if(mat == null)
            {
                mat = new SquareCardMat();
            }

            this.isSinglePlayer = isSinglePlayer;

            if (robots != null && robots.Count > 0)
            {
                this.robots = robots;

                foreach(IRobot robot in Robots)
                {
                    robot.SetMat(mat);
                }

                if (IsSinglePlayer)
                {
                    SetActiveRobot((robots.FirstOrDefault() as IHaveIdentifier).Guid);
                }
            }
        }

        public void SetActiveRobot(Guid guid)
        {
            if (robots != null && robots.Count > 0)
            {
                activeRobot = robots.Where(x => (x as IHaveIdentifier).Guid == guid).FirstOrDefault();
            }
        }

        private void OnCommandExecuted(object sender, string message)
        {
            CommandExecuted?.Invoke(sender, message);
        }

        public void ExecuteCommands(List<IRobotCommand> commands)
        {
            foreach(IRobotCommand command in commands)
            {
                string message = command.Execute(ActiveRobot);
                OnCommandExecuted(this, message);
            }
        }
    }
}
