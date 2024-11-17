public interface IContext<T> : IContext
{
    public T Request { get; }
}

public interface IContext
{
    public string PlayFabId { get; }

    public TitleAuthenticationContext TitleAuthenticationContext { get; }
    
}