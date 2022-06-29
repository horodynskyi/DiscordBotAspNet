using Infrastructure.Services.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DiscordBotWebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GenericGetController : ControllerBase
	{
		private readonly GenericGetService _genericGetService;

		public GenericGetController(GenericGetService genericGetService) 
		{
			_genericGetService = genericGetService;
		}

		[HttpGet("{type}")]
		public async Task<IActionResult> GetAllAsync(string type)//This metod was created in live share mode by 2 clowns
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
				var res = await _genericGetService.GetAllAsync(resType);
				return Ok(res);
			} 
			else 
			{
				return BadRequest("Bad type");
			}
		}
	}
}
