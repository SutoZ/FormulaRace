using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Pilots
{
   public class PilotListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public string Teamname { get; set; }

        public PilotListDto(Pilot pilot)
        {
            Id = pilot.Id;
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
            Teamname = pilot.Team?.Name;
        }
    }
}
