using Discord.WebSocket;
using DiscordBotWebApi.Bot;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DiscordBotWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BotAccessSettingsController : ControllerBase
    {
        [HttpPost("SetAdminAccount")]
        public IActionResult SetAdminAccount(string id)
        {
            AdminService.SetAdminData(id);
            return Ok();
        }
    }
}
