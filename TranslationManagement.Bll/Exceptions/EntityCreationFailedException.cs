namespace TranslationManagement.Bll.Exceptions;

public class EntityCreationFailedException : Exception
{
    public EntityCreationFailedException(Type entityType) : base($"Failed to create entity of type {entityType.FullName}.")
    {
        
    }
}