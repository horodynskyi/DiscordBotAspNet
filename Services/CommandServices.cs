using Discord.WebSocket;
using Services.Commands;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CommandServices
    {
        private readonly List<ICommand> _commands;

        public CommandServices()
        {
            _commands = new List<ICommand>{
               new RandomCommand(),
               new BulkDelete(),
               new GetHistoryCommand(),
               new SendImageCommand(),
               new FetchCalendarData(),
            };
        }

        public ICommand GetComand(SocketSlashCommand msg)
        {
            foreach (var command in _commands)
            {
                if (command.Contains(msg))
                {
                    command.SetParameters();
                    return command;
                }
            }

            return null;
        }
    }
}
