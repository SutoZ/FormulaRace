using AutoMapper;
using TeamManagementService.Application.Dtos.Pilots;
using TeamManagementService.Application.Dtos.Teams;
using TeamManagementService.Domain.Models;

namespace TeamManagementService.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Pilot, PilotListDto>().ReverseMap();
        CreateMap<Pilot, PilotCreateDto>().ReverseMap();
        CreateMap<Pilot, PilotDetailsDto>().ReverseMap();
        
        CreateMap<Pilot, PilotUpdateDto>();
        CreateMap<PilotUpdateDto, Pilot>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Team, TeamDetailsDto>().ReverseMap();
        CreateMap<Team, TeamListDto>().ReverseMap();
        CreateMap<Team, TeamUpdateDto>().ReverseMap();
        CreateMap<Team, TeamCreateDto>().ReverseMap();
    }
}