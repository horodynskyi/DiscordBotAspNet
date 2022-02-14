using Discord.WebSocket;
using Infrastructure.Commands;
using Infrastructure.Commands.RandomCommands;
using Infrastructure.Commands.Shikimory;
using Interfaces;

namespace Infrastructure.Services
{
    public class CommandServices
    {
        private readonly List<ICommand> _commands;

        public CommandServices()
        {
            _commands = new List<ICommand>{
               new RandomCommand(),
               new RollCommands(),
               new BulkDelete(),
               new GetHistoryCommand(),
               new FetchCalendarDataCommand(),
               new SetupSlashCommands(),
               new RandomAnimeArt(),
               new SendImageCommand(),
            };
        }

        public ICommand GetComand(SocketSlashCommand msg)
        {
            foreach (var command in _commands)
            {
                if (command.Name == msg.CommandName)
                {
                    return command;
                }
            }

            return null;
        }

        public ICommand GetComand(SocketMessage msg)
        {
            foreach (var command in _commands)
            {
                if (command.Contains(msg))
                {
                    return command;
                }
            }

            return null;
        }
    }
}
