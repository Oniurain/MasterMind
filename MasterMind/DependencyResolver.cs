namespace MasterMind
{
    using Ninject.Modules;
    using Services.Interfaces;
    using Services.Services;

    public class DependencyResolver : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadService>().To<ReadService>();
            Bind<IWriteService>().To<WriteService>();
            Bind<IRefereeService>().To<RefereeService>();
            Bind<IEngineService>().To<EngineService>();
        }
    }
}
