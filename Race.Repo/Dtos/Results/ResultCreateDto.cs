using Race.Model.Models;

namespace Race.Repo.Dtos.Results
{
    public class ResultCreateDto
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int PilotId { get; set; }
        public Result CreateModelObject()
        {
            return new Result(PilotId, Id);
        }
    }
}
