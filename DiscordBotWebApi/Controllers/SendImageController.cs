using Discord.WebSocket;
using DiscordBotWebApi.Bot;
using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBotWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SendImageController : ControllerBase
    {
        private readonly CommandsHandler _commandsHandler;
        public SendImageController(DiscordSocketClient client, CommandsHandler commandsHandler)
        {
            _commandsHandler = commandsHandler;
        }

        [HttpPost]
        public IActionResult SendImage(string url)
        {
            try
            {
                _commandsHandler.Handler(new SendImageCommand(), url);
            }
            catch (Exception)
            {

            }
            return Ok();
        }
    }
}
