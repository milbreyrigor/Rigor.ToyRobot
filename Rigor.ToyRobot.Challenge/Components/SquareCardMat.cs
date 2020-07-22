﻿using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rigor.ToyRobot.Challenge.Components
{
    public class SquareCardMat : IMat, IHaveIdentifier
    {
        public uint Height => 5;

        public uint Width => 5;

        public string Name => "Square Card";

        public Guid Guid => new Guid("{6933B37C-8109-4F3E-A262-DCDF44AF55E7}");

        public bool IsValidPosition(MatLocation position)
        {
            return position.X >= 0 && position.Y >= 0 && position.Y < this.Height && position.X < this.Width;
        }
    }
}
