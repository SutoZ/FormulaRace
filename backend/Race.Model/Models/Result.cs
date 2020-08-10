﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Race.Model.Models
{
    public class Result
    {
        [Key]
        [Required]
        public Guid ResultId { get; set; }

        [ForeignKey("Race")]
        public int RaceId { get; set; }

        public virtual Pilot Pilot { get; set; }

        [ForeignKey("Pilot")]
        public Guid PilotId { get; set; }

        public Result(Guid resultId, Guid pilotId)
        {
            this.ResultId = resultId;
            this.PilotId = pilotId;
        }
    }
}