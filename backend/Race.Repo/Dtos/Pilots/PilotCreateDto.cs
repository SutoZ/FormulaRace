using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }

        public PilotCreateDto(Pilot pilot)
        {
            Id = pilot.Id;
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        }
        public PilotCreateDto()
        {

        }

        public Pilot CreateModelObject() => new Pilot
        {
            Code = Code,
            Name = Name,
            Nationality = Nationality,
            Number = Number,
            Id = Id,
        };
    }
}
