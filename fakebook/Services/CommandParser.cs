using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using fakebook.Models;


namespace fakebook.Services
{
    class CommandParser
    {


        public CommandRequest ParseCommand(string input)
        {
            var splitCommand = input.Split(" ");

            var username = splitCommand[0];
            var command = splitCommand[1].Substring(1); // remove slash
            var commandArgs = splitCommand.Skip(2).Aggregate("", (current, next) => $"{current} {next}").TrimStart();

            return new CommandRequest(){username = username, command = (RequestCommand)Enum.Parse(typeof(RequestCommand), command), commandArgs = commandArgs};
        }


    }
}
