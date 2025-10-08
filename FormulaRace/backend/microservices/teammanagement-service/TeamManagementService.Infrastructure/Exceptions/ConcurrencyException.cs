namespace TeamManagementService.Infrastructure.Exceptions;

public class ConcurrencyException(string entityName, int entityId) : Exception(
    $"Entity '{entityName}' with ID '{entityId}' has been modified by another process. Please reload the entity and try again.")
{
    public string EntityName { get; set; } = entityName;
    public int EntityId { get; set; } = entityId;
}