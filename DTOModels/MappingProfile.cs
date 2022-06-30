using AutoMapper;
using Discord.WebSocket;

namespace DTOModels
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<AdministrationGuildDTO, SocketGuild>();
			CreateMap<SocketGuild, AdministrationGuildDTO>();
		}
	}
}
