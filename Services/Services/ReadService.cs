namespace Services.Services
{
    using Interfaces;
    using System;

    public class ReadService : IReadService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
