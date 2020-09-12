using Race.Model.Models;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotCreateDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public int TeamId { get; set; }

        //public PilotCreateDto(Pilot pilot)
        //{
        //    Name = pilot.Name;
        //    Number = pilot.Number;
        //    Code = pilot.Code;
        //    Nationality = pilot.Nationality;
        //    TeamId = pilot.TeamId;
        //}

        public Pilot CreateModelObject() => new Pilot(Name, Number, Code, Nationality);
    }
}
