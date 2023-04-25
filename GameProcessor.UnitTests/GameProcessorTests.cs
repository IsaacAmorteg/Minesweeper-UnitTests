using NUnit.Framework;
using Minesweeper.Core;
using Minesweeper.Core.Enums;

namespace Minesweeper.UnitTests
{
    public class GameProcessorTests
    {
        [Test]
        public void T1_Open_WhenCalledWithMine_ReturnsLose()
        {
            // Precondition
            var boolField = new bool[,]
            { 
                { false, false},
                { false, true}
            };

            var game = new GameProcessor(boolField);

            // Action
            var result = game.Open(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(GameState.Lose));
        }
    }
}