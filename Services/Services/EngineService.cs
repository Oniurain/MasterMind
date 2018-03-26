namespace Services.Services
{
    using Interfaces;
    using System.Linq;

    public class EngineService : IEngineService
    {
        private readonly IReadService _readService;
        private readonly IRefereeService _refereeService;
        private readonly IWriteService _writeService;

        public EngineService(IReadService readService, IWriteService writeService, IRefereeService refereeService)
        {
            _readService = readService;
            _writeService = writeService;
            _refereeService = refereeService;
        }

        public string GetCombination()
        {
            string combination = _readService.ReadLine();
            while (!_refereeService.IsValid(combination))
            {
                _writeService.Write("Rule 4 chars picked in this list: r,j,v,b,o,n :");
                combination = _readService.ReadLine();
            }

            return combination;
        }

        public bool UpdateBoard(Board board, string play)
        {
            board.Round++;
            board.BlackPegsCount = board.WhitePegsCount = 0;
            if (board.Combination == play)
            {
                board.BlackPegsCount = play.Length;
                return true;
            }
            char[] playChars = play.ToCharArray();
            char[] combinationValues = board.Combination.ToCharArray();
            for (int i = 0; i < playChars.Length; i++)
            {
                if (playChars[i] == combinationValues[i])
                {
                    board.BlackPegsCount++;
                }
                else if (combinationValues.Contains(playChars[i]) && combinationValues.Count(p => p == playChars[i]) >= playChars.Count(p => p == playChars[i]))
                {
                    board.WhitePegsCount++;
                }
            }

            return false;
        }

        public void PrintBoard(Board board, string lastPlay)
        {
            _writeService.Write($"{lastPlay}|{board.BlackPegsCount}|{board.WhitePegsCount}|{board.Round}/{Board.MAX_ROUND}");
        }
    }
}
