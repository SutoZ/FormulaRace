using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
      //  public List<CreateResultDto> Results { get; set; } = new List<CreateResultDto>();

        public PilotCreateDto(Pilot pilot)
        {
            Id = pilot.PilotId;
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        //    Results = Results.CreateModelObject();
        }
    }
}
