using Race.Model.Models;


namespace Race.Repo.Dtos.Results
{
    public class ResultListDto
    {
        public int RaceId { get; set; }
        public int PilotId { get; set; }

        public virtual Pilot Pilot { get; set; }
        public ResultListDto(Result result)
        {
            RaceId = result.RaceId;
            PilotId = result.PilotId;
        }
    }
}
