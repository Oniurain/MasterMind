namespace ServicesTests.Services
{
    using global::Services.Services;
    using NUnit.Framework;
    using Moq;
    using global::Services.Interfaces;
    using global::Services;

    public class TestEngineService
    {
        private Mock<IReadService> _readService;
        private Mock<IWriteService> _writeServiceMock;
        private Mock<IRefereeService> _refereeService;
        private EngineService _testedService;

        [SetUp]
        public void SetUp()
        {
            _readService = new Mock<IReadService>();
            _writeServiceMock = new Mock<IWriteService>();
            _refereeService = new Mock<IRefereeService>();
            _testedService = new EngineService(_readService.Object, _writeServiceMock.Object, _refereeService.Object);
        }

        [Test]
        public void EngineService_UpdateBoard_SimpleBoardGoodPlay_ReturnTrue()
        {
            const string PLAY = "rjbn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, PLAY);

            Assert.IsTrue(result);
            Assert.AreEqual(PLAY.Length, board.BlackPegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_SimpleBoardMissPlay_ReturnFalseNoPegs()
        {
            const string PLAY = "rjbn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, "aaaa");

            Assert.IsFalse(result);
            Assert.AreEqual(0, board.BlackPegsCount);
            Assert.AreEqual(0, board.WhitePegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_SimpleBoardNearGood_ReturnFalseWhitePegs()
        {
            const string PLAY = "rjbn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, "aarj");

            Assert.IsFalse(result);
            Assert.AreEqual(0, board.BlackPegsCount);
            Assert.AreEqual(2, board.WhitePegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_SimpleBoardSomeGoods_ReturnFalseBlackPegs()
        {
            const string PLAY = "rjbn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, "rjaa");

            Assert.IsFalse(result);
            Assert.AreEqual(2, board.BlackPegsCount);
            Assert.AreEqual(0, board.WhitePegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_SimpleBoardSomeGoodsAndSomeNear_ReturnFalseBlackPegsAndWhitePegs()
        {
            const string PLAY = "rjbn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, "rjnb");

            Assert.IsFalse(result);
            Assert.AreEqual(2, board.BlackPegsCount);
            Assert.AreEqual(2, board.WhitePegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_ComplexBoardSomeGoodsAndSomeNear_ReturnFalseBlackPegsAndWhitePegs()
        {
            const string PLAY = "bbnn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, "bbbn");

            Assert.IsFalse(result);
            Assert.AreEqual(3, board.BlackPegsCount);
            Assert.AreEqual(0, board.WhitePegsCount);
        }

        [Test]
        public void EngineService_UpdateBoard_ComplexBoardGoodPlay_ReturnTrue()
        {
            const string PLAY = "bbnn";
            var board = new Board(PLAY);

            bool result = _testedService.UpdateBoard(board, PLAY);

            Assert.IsTrue(result);
            Assert.AreEqual(PLAY.Length, board.BlackPegsCount);
        }

        [Test]
        public void EngineService_PrintBoard_Execute_GoodFormat()
        {
            const string PLAY = "bbnn";
            var board = new Board(PLAY);
            const string LAST_PLAY = "azea";

            _testedService.PrintBoard(board, LAST_PLAY);

            _writeServiceMock.Verify(p => p.Write($"{LAST_PLAY}|{board.BlackPegsCount}|{board.WhitePegsCount}|{board.Round}/{Board.MAX_ROUND}"), Times.Once);
        }

        [Test]
        public void EngineService_GetCombination_CombinationIsValid_ReturnCombinationNoInput()
        {
            const string INPUT = "foo";
            _readService.Setup(p => p.ReadLine()).Returns(INPUT);
            _refereeService.Setup(p => p.IsValid(INPUT)).Returns(true);

            string result = _testedService.GetCombination();

            Assert.AreEqual(INPUT, result);
            _writeServiceMock.Verify(p => p.Write(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void EngineService_GetCombination_CombinationIsNotValid_ReturnCombinationInput()
        {
            const string INPUT = "foo";
            const string VALID_INPUT = "bar";
            _readService.SetupSequence(p => p.ReadLine()).Returns(INPUT).Returns(VALID_INPUT);
            _refereeService.Setup(p => p.IsValid(INPUT)).Returns(false);
            _refereeService.Setup(p => p.IsValid(VALID_INPUT)).Returns(true);

            string result = _testedService.GetCombination();

            Assert.AreEqual(VALID_INPUT, result);
            _writeServiceMock.Verify(p => p.Write("Rule 4 chars picked in this list: r,j,v,b,o,n :"), Times.Once);
            _readService.Verify(p => p.ReadLine(), Times.Exactly(2));
        }
    }
}
