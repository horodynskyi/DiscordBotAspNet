using Discord.WebSocket;
using DiscordBotWebApi.Bot;
using Microsoft.AspNetCore.Mvc;
using Services.Commands;

namespace DiscordBotWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendImageController : ControllerBase
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandsHandler _commandsHandler;
        public SendImageController(DiscordSocketClient client, CommandsHandler commandsHandler)
        {
            _client = client;
            _commandsHandler = commandsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> SendImage(string url) 
        {
            try
            {
                await _commandsHandler.Handler(new SendImageCommand(), url);
            }
            catch (Exception) { 
            
            }
            return Ok();
        }
    }
}
