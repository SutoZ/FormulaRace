using Race.Model.Models;
using System;

namespace Race.Repo.Dtos.Results
{
    public class ResultCreateDto
    {
        public Guid Id { get; set; }
        public int RaceId { get; set; }
        public Guid PilotId { get; set; }
        public Result CreateModelObject()
        {
            return new Result(PilotId, Id);
        }
    }
}
