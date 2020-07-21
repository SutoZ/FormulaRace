using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Race.Model.Models
{
    public class Result //: BaseEntity
    {
        [Key]
        [Required]
        public int ResultId { get; set; }

        [ForeignKey("Race")]
        public int RaceId { get; set; }

        public virtual Pilot Pilot { get; set; }
        [ForeignKey("Pilot")]
        public int PilotId { get; set; }

        public Result(int resultId, int pilotId)// : base(id)
        {
            this.ResultId = resultId;
            this.PilotId = pilotId;
        }
    }
}
