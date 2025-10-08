namespace TeamManagementService.Domain.Interfaces;

public interface IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}