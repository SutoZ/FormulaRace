using AutoMapper;
using Race.Model.Models;
using Race.Repo.Dtos.Pilots;

namespace Race.Web.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pilot, PilotListDto>(); //.ForMember(dest => dest.TeamDto, act => act.MapFrom(src => src.Team));
        }
    }
}
