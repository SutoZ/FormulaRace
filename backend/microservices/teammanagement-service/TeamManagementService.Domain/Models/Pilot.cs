using System.Text.Json.Serialization;

namespace TeamManagementService.Domain.Models;

public class Pilot : Entity
{
    public string Number { get; set; }
    public string Code { get; set; }
    public string Nationality { get; set; }

    [JsonIgnore]
    public virtual Team Team { get; set; }

    //[ForeignKey("Team")]  Set in configuration
    public int TeamId { get; set; }

    public Pilot() { }

    public Pilot(string name, string number, string code, string nationality, int teamId)
    {
        Name = name;
        Number = number;
        Code = code;
        Nationality = nationality;
        TeamId = teamId;
    }
}