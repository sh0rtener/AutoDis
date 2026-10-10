namespace AutoDis.Domain;

public abstract class CoreException : Exception
{
    public CoreException(string message)
        : base(message) { }
}
