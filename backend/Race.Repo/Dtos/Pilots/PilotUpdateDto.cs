using Race.Model.Models;

namespace Race.Repo.Dtos.Pilots
{
    public class PilotUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }

        //public PilotUpdateDto(Pilot pilot)
        //{
        //    Name = pilot.Name;
        //    Id = pilot.Id;
        //    Number = pilot.Number;
        //    Code = pilot.Code;
        //    Nationality = pilot.Nationality;
        //}

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
