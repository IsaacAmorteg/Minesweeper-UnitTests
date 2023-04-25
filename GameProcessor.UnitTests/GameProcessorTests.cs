using NUnit.Framework;
using Minesweeper.Core;
using Minesweeper.Core.Enums;

namespace Minesweeper.UnitTests
{
    public class GameProcessorTests
    {

        [Test]
        public void T1_Open_WhenOpenCellIfGameStateIsNotActive_ReturnsException()
        {
            // Precondition
            var boolField = new bool[2, 2];

            var game = new GameProcessor(boolField);

            game.Open(0, 0);

            // Assert
            Assert.Throws<InvalidOperationException>(() => game.Open(1, 1));
        }

        [Test]
        public void T2_Open_WhenCalledWithMine_ReturnsLose()
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
        [Test]
        public void T3_Open_WhenAllNonMineCellsAreOpened_ReturnsWin()
        {
            // Precondition
            var boolField = new bool[,]
            {
                { false, false},
                { true, false}
            };

            var game = new GameProcessor(boolField);

            // Action
            var result = game.Open(1, 1);
            result = game.Open(1,0);
            result = game.Open(0, 0);

            // Assert
            Assert.That(result, Is.EqualTo(GameState.Win));
        }

    }
}