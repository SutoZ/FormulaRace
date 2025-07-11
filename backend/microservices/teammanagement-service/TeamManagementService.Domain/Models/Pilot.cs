using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeamManagementService.Domain.Models;

public class Pilot
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Code { get; set; }
    public string Nationality { get; set; }

    [JsonIgnore]
    public virtual Team Team { get; set; }

    [ForeignKey("Team")]
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