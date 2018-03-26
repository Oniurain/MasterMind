namespace Services.Services
{
    using System;
    using Interfaces;

    public class WriteService : IWriteService
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }
    }
}
