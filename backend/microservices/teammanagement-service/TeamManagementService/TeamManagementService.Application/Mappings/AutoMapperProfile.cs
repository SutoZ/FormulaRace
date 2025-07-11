using AutoMapper;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pilot, PilotListDto>();
        CreateMap<Pilot, PilotDetailsDto>();

        CreateMap<Team, TeamDetailsDto>();
    }
}