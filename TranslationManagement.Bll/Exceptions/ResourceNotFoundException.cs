namespace TranslationManagement.Bll.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(Type resourceType)
    {
        MissingResourceType = resourceType;
    }

    public Type MissingResourceType { get; }
}