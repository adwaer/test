namespace In.Cqrs.Query
{
    public interface IMultipleQueryResult<T>
    {
        T[] Data { get; set; }
    }
}
