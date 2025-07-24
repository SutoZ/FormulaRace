using System.Text.Json.Serialization;

namespace TeamManagementService.Domain.Models;

public class Team : Entity
{
    public DateTime DateOfFoundation { get; set; }
    public string OwnerName { get; set; }
    public int ChampionShipPoints { get; set; }

    [JsonIgnore]
    public virtual List<Pilot> Pilots { get; set; }
}