using Discord.WebSocket;
using Infrastructure.Commands;
using Infrastructure.Commands.Images;
using Infrastructure.Commands.RandomCommands;
using Infrastructure.Commands.Shikimory;
using Infrastructure.Models;
using Interfaces;

namespace Infrastructure.Services
{
    public class CommandService
    {
        public List<ICommand> Commands { get; }

        public CommandService(ShikimoryService shikimoryService, UserService userService)
        {
            Commands = new List<ICommand>{
               new RandomCommand(),
               new RollCommands(),
               new BulkDelete(),
               new GetHistoryCommand(),
               new FetchCalendarDataCommand(shikimoryService),
               new SetupCommand(this, userService),
               new SearchAnimeArtCommand(),
               new RandomAnimeArt(),
               new SearchAnimeCommand(shikimoryService),
               new JoinToVoiceChat(),
            };
        }

        public ICommand GetComand(SocketSlashCommand msg)
        {
            foreach (var command in Commands)
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
            foreach (var command in Commands)
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
