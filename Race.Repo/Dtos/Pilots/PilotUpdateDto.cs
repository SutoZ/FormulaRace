using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotUpdateDto
    {
       // public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public List<Result> Results { get; set; }

        public Pilot UpdateModelObject(Pilot pilot)
        {
            //pilot.PilotId = Id;
            pilot.Name = Name;
            pilot.Number = Number;
            pilot.Code = Code;
            pilot.Nationality = Nationality;

            return pilot;
        }
    }
}
