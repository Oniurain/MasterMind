namespace Services.Interfaces
{
    public interface IEngineService
    {
        string GetCombination();
        bool UpdateBoard(Board board, string play);
        void PrintBoard(Board board, string play);
    }
}
