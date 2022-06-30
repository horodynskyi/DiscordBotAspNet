using Discord.WebSocket;
using Infrastructure.Services.Administration;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DiscordBotWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GetController : ControllerBase
	{
		private readonly GetService _getService;

		public GetController(GetService getService) 
		{
			_getService = getService;
		}

		[HttpGet("{type}")]
		public async Task<IActionResult> GetAllAsync(string type)
		{
			Type resType = null;
			foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
			{
				resType = a.GetTypes().FirstOrDefault(x => x.Name.ToLower() == type.ToLower());
				if (resType is not null)
					break;
			}

			if (resType is not null)
			{
				var res = await _getService.GetAllAsync(resType);
				return Ok(res);
			} 
			else 
			{
				return BadRequest("Bad type");
			}
		}
	}
}
