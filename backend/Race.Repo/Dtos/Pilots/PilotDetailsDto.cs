﻿using Race.Model.Models;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public PilotDetailsDto() { }
        public PilotDetailsDto(Pilot pilot)
        {
            Id = pilot.Id;
            Name = pilot.Name;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
            Number = pilot.Number;
        }
    }
}
