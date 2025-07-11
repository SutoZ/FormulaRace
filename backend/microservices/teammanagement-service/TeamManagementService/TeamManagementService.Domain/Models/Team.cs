using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeamManagementService.Domain.Models;

public class Team
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfFoundation { get; set; }
    public string OwnerName { get; set; }
    public int ChampionShipPoints { get; set; }

    [JsonIgnore]
    public virtual List<Pilot> Pilots { get; set; }
}