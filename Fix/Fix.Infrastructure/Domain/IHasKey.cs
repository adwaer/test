namespace Fix.Infrastructure.Domain
{
	public interface IHasKey<out TId> : IHasKey
	{
		TId Id { get; }
	}

	public interface IHasKey
	{
	}
}