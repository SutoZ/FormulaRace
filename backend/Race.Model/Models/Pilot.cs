using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Race.Model.Models
{
    public class Pilot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public virtual List<Result> Results { get; set; }
        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public Pilot()
        {

        }
    }
}
