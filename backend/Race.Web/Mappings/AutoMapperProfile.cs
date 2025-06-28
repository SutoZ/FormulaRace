using AutoMapper;
using Race.Model.Models;
using Race.Repo.Dtos.Pilots;
using Race.Repo.Dtos.Teams;

namespace Race.Web.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pilot, PilotListDto>();
        CreateMap<Pilot, PilotDetailsDto>();

        CreateMap<Team, TeamDetailsDto>();
    }
}