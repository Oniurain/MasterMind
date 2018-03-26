namespace Services
{
    public class Board
    {
        public const int MAX_ROUND = 10;

        public string Combination { get; set; }

        public int Round { get; set; }

        public int BlackPegsCount { get; set; }

        public int WhitePegsCount { get; set; }

        public bool IsGameOver
        {
            get
            {
                return Round > MAX_ROUND;
            }
        }

        public Board(string combination)
        {
            Combination = combination;
        }
    }
}