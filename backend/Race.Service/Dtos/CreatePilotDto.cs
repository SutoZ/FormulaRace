using Race.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Race.Service.Dtos
{
    public class CreatePilotDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public List<Result> Results { get; set; } = new List<Result>();

        public CreatePilotDto(Pilot pilot)
        {
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
            Results = pilot.Results;
        }
    }
}
