namespace MasterMind
{
    using Services.Interfaces;
    using Services;

    public class Game
    {
        private readonly IEngineService _engineService;
        private readonly IWriteService _writeService;

        public Game(IWriteService writeService, IEngineService engineService)
        {
            _writeService = writeService;
            _engineService = engineService;
        }

        public void Start()
        {
            _writeService.Write("Hi bro, enter your 4 chars combination please :");
            string solution = _engineService.GetCombination();

            Board board = new Board(solution);
            while (!board.IsGameOver)
            {
                _writeService.Write("Hi bro, enter your 4 chars combination please :");
                string play = _engineService.GetCombination();
                bool isWinningMove = _engineService.UpdateBoard(board, play);
                if (isWinningMove)
                {
                    _writeService.Write("GG no re");
                    return;
                }

                _engineService.PrintBoard(board, play);
            }
        }
    }
}