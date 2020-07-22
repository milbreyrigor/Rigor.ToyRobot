
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rigor.ToyRobot.Challenge.Challenges;
using Rigor.ToyRobot.Challenge.Components;
using Rigor.ToyRobot.Common.Common;
using Rigor.ToyRobot.Common.Data;
using Rigor.ToyRobot.Common.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

using TestStack.BDDfy;

using Xunit;

namespace Rigor.ToyRobot.Test
{
    public class ToyRobotChallengeTest
    {
        private IChallenge _challenge;

        [Fact]
        public void TestPlaceSuccess()
        {
            this.Given(s => s.ChallengeIsInitialized())
                .When(s => s.SetRobotAt(new MatPosition(new MatLocation(3, 3), Directions.East)))
                .Then(s => s.ReportResultsTo(3, 3, Directions.East))
                .BDDfy();
        }

        [Theory]
        [InlineData(5, 5, 3)]
        [InlineData(0, 5, 3)]
        [InlineData(5, 0, 3)]
        public void TestPlaceOutside(uint x, uint y, int directionValue)
        {
            this.Given(s => s.ChallengeIsInitialized())
                .When(s => s.SetRobotAt(new MatPosition(new MatLocation(x, y), directionValue)))
                .Then(s => s.ReportResultsTo(0, 0, Directions.North))
                .BDDfy();
        }

        [Theory]
        [InlineData(0, 0, 3, 1, 0)]
        [InlineData(0, 0, 0, 0, 1)]
        [InlineData(4, 4, 2, 3, 4)]
        [InlineData(4, 4, 1, 4, 3)]
        public void TestMove(uint x, uint y, int directionValue, uint newX, uint newY)
        {
            this.Given(s => s.ChallengeIsInitialized())
                .And(s => s.SetRobotAt(new MatPosition(new MatLocation(x, y), directionValue)))
                .When(s => s.RobotMoves())
                .Then(s => s.ReportResultsTo(newX, newY, Directions.GetAll().Where(d => d.Value == directionValue).FirstOrDefault()))
                .BDDfy();
        }

        [Theory]
        [InlineData(0, 0, 2)]
        [InlineData(0, 0, 1)]
        [InlineData(4, 4, 3)]
        [InlineData(4, 4, 0)]
        public void TestMoveOutsideBoundary(uint x, uint y, int directionValue)
        {
            this.Given(s => s.ChallengeIsInitialized())
                .And(s => s.SetRobotAt(new MatPosition(new MatLocation(x, y), directionValue)))
                .When(s => s.RobotMoves())
                .Then(s => s.ReportResultsTo(x, y, Directions.GetAll().Where(d => d.Value == directionValue).FirstOrDefault()))
                .BDDfy();
        }

        private void ChallengeIsInitialized()
        {
            _challenge = new SquareCardMatChallenge();
            List<IRobot> robots = new List<IRobot>()
            {
                new Robot(_challenge.Mat, "TestRobot", new Guid("{44684A66-7BF3-4F6B-969D-BC0F40CAEC10}"))
            };

            _challenge.Initialize(robots);
        }

        private void RobotMoves()
        {
            _challenge.ActiveRobot.Move();
        }

        private void ReportResultsTo(uint x, uint y, Direction facing)
        {
            var report = _challenge.ActiveRobot.CurrentPosition;
            Xunit.Assert.Equal(x, report.Location.X);
            Xunit.Assert.Equal(y, report.Location.Y);
            Xunit.Assert.Equal(facing, report.Direction);
        }

        private void SetRobotAt(MatPosition position)
        {
            _challenge.ActiveRobot.SetPosition(position);
        }
    }
}
