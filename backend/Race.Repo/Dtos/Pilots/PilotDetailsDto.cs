using Race.Model.Models;
using System;
using System.Collections.Generic;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public List<Result> Results { get; set; }
        public PilotDetailsDto(Pilot pilot)
        {
            Id = pilot.PilotId;
            Name = pilot.Name;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        }
    }
}
