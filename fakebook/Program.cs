using System;
using fakebook.Services;

namespace fakebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandParser = new CommandParser();
            var commandExecuter = new commandExecuter();


            while (true)
            {
               var request = commandParser.ParseCommand(Console.ReadLine());

               commandExecuter.executeCommand(request);
            }

        }
    }
}
