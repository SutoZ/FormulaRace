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
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Code { get; set; }
        public string Nationality { get; set; }
        public virtual Team Team { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public Pilot()
        {

        }

        public Pilot(string name, string number, string code, string nationality /*, Team team, int teamId*/)
        {
            Name = name;
            Number = number;
            Code = code;
            Nationality = nationality;
            //Team = team;
            //TeamId = teamId;
        }
    }
}
