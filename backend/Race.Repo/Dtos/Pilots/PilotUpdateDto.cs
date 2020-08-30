using Race.Model.Models;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotUpdateDto
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }

        public PilotUpdateDto(Pilot pilot)
        {
            Name = pilot.Name;
            Number = pilot.Number;
            Code = pilot.Code;
            Nationality = pilot.Nationality;
        }

        public Pilot UpdateModelObject(Pilot pilot)
        {
            pilot.Name = Name;
            pilot.Number = Number;
            pilot.Code = Code;
            pilot.Nationality = Nationality;

            return pilot;
        }
    }
}
