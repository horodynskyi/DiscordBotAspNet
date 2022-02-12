using Discord.WebSocket;
using DiscordBotWebApi.Bot;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DiscordBotWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BotAccesSettingsController : ControllerBase
    {
        private readonly AdminService _adminService;
        public BotAccesSettingsController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("SetAdminAccount")]
        public IActionResult SetAdminAccount(string id)
        {
            _adminService.SetAdminData(id);
            return Ok();
        }
    }
}
