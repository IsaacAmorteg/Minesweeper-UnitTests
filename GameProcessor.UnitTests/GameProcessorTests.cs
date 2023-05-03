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

        [Test]
        public void T1_Open_WhenCalledWithMine_ReturnsLose()
        {
            //Action
            var result = _game.Open(0, 1);

            // Assert
            Assert.That(result, Is.EqualTo(GameState.Lose));
        }
        [Test]
        public void T2_Open_WhenAllNonMineCellsAreOpened_ReturnsWin()
        {
            // Action
            var result = _game.Open(0, 0);
            result = _game.Open(1, 0);
            result = _game.Open(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(GameState.Win));
        }

        [Test]
        public void T3_Open_WhenCalledWithNoMine_ReturnsActive()
        {
            // Action
            var result = _game.Open(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(GameState.Active));
        }


        [Test]
        public void T4_Open_WhenCalledWithNoMineAndOneMineNeighbor_SetMineNeighborCountToOne()
        {
            //Precondition
            _boolField = new bool[,]
             {
                { false, false, false},
                { false, true, false},
                { false, false, false }
             };

            _game = new GameProcessor(_boolField);

            //Action
            _game.Open(0, 0);

            //Assert
            Assert.That(_game.GetCurrentField()[0, 0], Is.EqualTo(PointState.Neighbors1));
        }
        [Test]
        public void T5_GetCurrentField_WhenCalledWithNoMineAndNoMineNeighbors_OpensAllSurroundingCells()
        {
            //Precondition
            _boolField = new bool[,]
             {
                { false, false, false},
                { false, false, false},
                { false, false, false }
             };

            _game = new GameProcessor(_boolField);
            _game.Open(0, 0);

            // Action & Assert
            Assert.That(_game.GetCurrentField(), Is.EqualTo(new PointState[,]
            {
                {PointState.Neighbors0, PointState.Neighbors0, PointState.Neighbors0},
                {PointState.Neighbors0, PointState.Neighbors0, PointState.Neighbors0},
                {PointState.Neighbors0, PointState.Neighbors0, PointState.Neighbors0}
            }));
        }

        [Test]
        public void T6_GetCurrentField_WhenCalledWithNoMineAndMultipleMineNeighbor_SetMineNeighborCountToCorrectValue()
        {
            //Precondition
            _boolField = new bool[,]
             {
                { true, false, true},
                { false, false, false},
                { true, false, true}
             };

            _game = new GameProcessor(_boolField);
            _game.Open(1, 1);

            //Action & Assert
            Assert.That(_game.GetCurrentField()[1, 1], Is.EqualTo(PointState.Neighbors4));
        }

        [Test]
        public void T7_GetCurrentField_WhenNoCellIsOpen_ReturnsClosedFieldState()
        {
            //Precondition
            _boolField = new bool[,]
             {
                { false, false, false },
                { false, false, false },
                { false, false, false }
             };

            _game = new GameProcessor(_boolField);

            //Action
            var actual = _game.GetCurrentField();

            //Assert
            Assert.That(actual, Is.EqualTo(new PointState[,]
            {
                { PointState.Close, PointState.Close, PointState.Close },
                { PointState.Close, PointState.Close, PointState.Close },
                { PointState.Close, PointState.Close, PointState.Close }
            }));
        }

        [Test]
        public void T8_Open_WhenMineCellIsOpened_ReturnsMine()
        {
            //Precondition
            _boolField = new bool[,]
             {
                { true, false, false },
                { false, true, false },
                { false, false, false }
             };

            _game = new GameProcessor(_boolField);
            _game.Open(0, 0);

            //Action & Assert
            Assert.That(_game.GetCurrentField()[0, 0], Is.EqualTo(PointState.Mine));

        }

        [Test]
        public void T9_GetCurrentField_WhenMineCellIsOpened_ReturnsCorrectArray()
        {
            // Preconditions
            _boolField = new bool[,]
            {
                { true, false, false },
                { true, true, false },
                { false, false, false }
            };
            _game = new GameProcessor(_boolField);  
                                                    
            // Act
            _game.Open(0, 0);
            var actual = _game.GetCurrentField();

            // Assert
            Assert.That(actual, Is.EqualTo(new PointState[,]
            {
                 { PointState.Mine, PointState.Neighbors0, PointState.Neighbors0},
                 { PointState.Mine, PointState.Mine, PointState.Neighbors0 },
                 { PointState.Neighbors0, PointState.Neighbors0, PointState.Neighbors0 }
                                
            }));            
        }

        [Test]
        public void T80_GetCurrentField_WhenMineCellIsOpenWithNeighborsThenMineCellIsOpen_ReturnCellWithNeighborAndMinesAndNeigbor0Cells()
        {
            // Preconditions
            _boolField = new bool[,]
            {
                { true, false, false },
                { true, true, false },
                { false, false, false }
            };
            _game = new GameProcessor(_boolField);  
                                                   
                                                   
            // Act
            _game.Open(2, 2);
            _game.Open(1, 1);
            var actual = _game.GetCurrentField();

            // Assert
            Assert.That(actual, Is.EqualTo(new PointState[,]
            {
                 { PointState.Mine, PointState.Neighbors0, PointState.Neighbors0},
                 { PointState.Mine, PointState.Mine, PointState.Neighbors0 },
                 { PointState.Neighbors0, PointState.Neighbors0, PointState.Neighbors1 }

            }));
        }

        [Test]
        public void T90_GetCurrentField_WhenCalledWithNoMineAndMultipleMineNeighbor_SetMineNeighborCountToCorrectValue()
        {
            //Precondition
            _boolField = new bool[,]
             {
//                                            Y

                { true, false, false },//     0
                { true, true, false },//      1
                { false, false, false }//     2

//           X     0        1      2 


             };

            _game = new GameProcessor(_boolField);
            _game.Open(2, 0);
            _game.Open(1, 0);
            var actual = _game.GetCurrentField();

            //Action & Assert
            Assert.That(actual, Is.EqualTo(new PointState[,] 
            {
                 { PointState.Close, PointState.Neighbors3, PointState.Neighbors1}, 
                 { PointState.Close, PointState.Close, PointState.Close },
                 { PointState.Close, PointState.Close, PointState.Close }

            }));
        }





        // Print Check - Not test

        public void PrintCurrentFieldToConsole()
        {
            _boolField = new bool[,]
            {
                { true, false, false },
                { true, true, false },
                { false, false, false }
            };
            _game = new GameProcessor(_boolField);

            _game.Open(2, 0);
            var currentField = _game.GetCurrentField();
            

            // Print the current field to the console
            for (int i = 0; i < currentField.GetLength(0); i++)
            {
                for (int j = 0; j < currentField.GetLength(1); j++)
                {
                    Console.Write(currentField[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}