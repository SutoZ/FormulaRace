using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Dtos.Pilots
{
   public class PilotListDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }

        public PilotListDto(Pilot pilot)
        {
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        }
    }
}
