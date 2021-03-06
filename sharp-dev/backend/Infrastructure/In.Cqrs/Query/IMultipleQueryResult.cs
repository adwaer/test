﻿namespace In.Cqrs.Query
{
    public interface IMultipleQueryResult<T>
    {
        T[] Data { get; set; }
    }

    public class MultipleQueryResult<T> : IMultipleQueryResult<T>
    {
        public int Count { get; set; }
        public T[] Data { get; set; }
    }
}
