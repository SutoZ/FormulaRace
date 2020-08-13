using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Pilots
{
   public class PilotListDto
    {
        public Guid PilotId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }

        public PilotListDto(Pilot pilot)
        {
            PilotId = pilot.Id;
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        }
    }
}
