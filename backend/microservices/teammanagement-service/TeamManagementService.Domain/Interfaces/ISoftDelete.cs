namespace TeamManagementService.Domain.Interfaces;

public interface ISoftDelete
{
    public bool Active { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public void Undo()
    {
        Active = true;
        DeletedAt = null;
    }
}