using TeamManagementService.Domain.Interfaces;

namespace TeamManagementService.Domain.Models;

public class Entity : IEntity, ISoftDelete, IAuditable
{
    public int Id { get; set; }
    public string Name { get; set; }

    // IAuditable properties
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    // ISoftDelete properties
    public bool Active { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}