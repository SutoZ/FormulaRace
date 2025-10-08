namespace TeamManagementService.Infrastructure.Exceptions;

public class EntityNotFundException(string entityName, int entityId) : Exception(
    $"Entity '{entityName}' with ID '{entityId}' has been modified by another process. Please reload the entity and try again.")
{
}