namespace MasterMind
{
    using Ninject;
    using Services.Interfaces;

    public class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new DependencyResolver());
            var writeService = kernel.Get<IWriteService>();
            var engineService = kernel.Get<IEngineService>();

            var game = new Game(writeService, engineService);
            game.Start();
        }
    }
}

