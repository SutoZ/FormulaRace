using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Race.Model.Models
{
    public class Team
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfFoundation { get; set; }
        public string OwnerName { get; set; }
        public int ChampionShipPoints { get; set; }
        public List<Pilot> Pilots { get; set; }
    }
}
