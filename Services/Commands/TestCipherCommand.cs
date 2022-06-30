using Discord;
using Discord.WebSocket;
using Infrastructure.Models;
using Infrastructure.Services;

namespace Infrastructure.Commands
{
    public class TestCipherCommand : DiscordSlashCommand
    {
        public override string Name => "testcipher";

        private readonly TokenService _tokenService;

        public TestCipherCommand(TokenService tokenService) 
        {
            _tokenService = tokenService;
        }

        public override async Task ExecuteAsync(DiscordSocketClient client, object commandObj)
        {
            if (commandObj is SocketSlashCommand command)
            {
                var data = command.Data.Options.First().Value.ToString();
                var res = _tokenService.Encrypt(data);
                var res1 = _tokenService.Decrypt(res);
                var res2 = _tokenService.GenerateToken(data);
                await command.RespondAsync(res +"\n"+ res1 + "\nToken = " + res2);
            }
            else
            {
                return;
            }
        }

        public override SlashCommandBuilder GetSlashCommandBuilder()
        {
            var testCipherCommand = new SlashCommandBuilder
            {
                Name = Name,
                Description = "return ciphered text",
            };

            testCipherCommand.AddOption(
                "text",
                ApplicationCommandOptionType.String,
                "Text",
                isRequired: true             
            );
            return testCipherCommand;
        }
    }
}
