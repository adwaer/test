namespace In.Cqrs
{
    public interface IEntity
    {
        object GetId();
    }

    public interface IEntity<TId> : IEntity
    {
        TId Id { get; set; }
        bool IsNew();
    }
}
