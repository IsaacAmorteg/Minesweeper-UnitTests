using NUnit.Framework;
using Minesweeper.Core;
using Minesweeper.Core.Enums;

namespace Minesweeper.UnitTests
{
    [TestFixture]
    public class GameProcessorTests
    {
        private bool[,] _boolField;
        private GameProcessor _game;

        [SetUp]
        public void SetUp() 
        {
            _boolField = new bool[,]
            {
                { false, false},
                { true, false}
            };

            _game = new GameProcessor(_boolField);
        }


/*        [Test]
        public void T1_Open_WhenOpenCellIfGameStateIsNotActive_ReturnsException() //Negative Scenario, pending for removal.
        {
            // Precondition
            var boolField = new bool[2, 2];

            var game = new GameProcessor(boolField);

            game.Open(0, 0);

            // Assert
            Assert.Throws<InvalidOperationException>(() => game.Open(1, 1));
        }*/
        
        [Test]
        public void T1_Open_WhenCalledWithMine_ReturnsLose()
        {
            // Precondition
            /*var boolField = new bool[,]
            { 
                { false, false},
                { false, true}
            };

            var game = new GameProcessor(boolField);*/

            // Action
           
            var result0 = _game.Open(0, 1);

            // Assert
            Assert.That(result0, Is.EqualTo(GameState.Lose));
        }
        [Test]
        public void T2_Open_WhenAllNonMineCellsAreOpened_ReturnsWin()
        {
/*            // Precondition
            var boolField = new bool[,]
            {
                { false, false},
                { true, false}
            };

            var game = new GameProcessor(boolField);*/

            // Action
            var result1 = _game.Open(0, 0);
            result1 = _game.Open(1,0);
            result1 = _game.Open(1, 1);

            // Assert
            Assert.That(result1, Is.EqualTo(GameState.Win));
        }

        [Test]
        public void T3_Open_WhenCalledWithNoMine_ReturnsActive()
        {
            // Precondition
/*            var boolField = new bool[,]
            {
                { false, false},
                { true, false}
            };

            var game = new GameProcessor(boolField);*/

            // Action
            var result2 = _game.Open(1, 1);
            
            // Assert
            Assert.That(result2, Is.EqualTo(GameState.Active));
        }

    }
}