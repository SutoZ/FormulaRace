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
        
        public Pilot CreateModelObject() => new Pilot(Name, Number, Code, Nationality, TeamId);
    }
}
