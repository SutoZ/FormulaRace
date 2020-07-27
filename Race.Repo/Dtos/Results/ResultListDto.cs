using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Results
{
    public class ResultListDto
    {
        public int RaceId { get; set; }
        public Guid PilotId { get; set; }

        public virtual Pilot Pilot { get; set; }
        public ResultListDto(Result result)
        {
            RaceId = result.RaceId;
            PilotId = result.PilotId;
        }
    }
}
