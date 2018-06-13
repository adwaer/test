using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fix.Infrastructure.Domain
{
	public abstract class HasKeyBase<TId> : IHasKey<TId>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual TId Id { get; set; }
	}
}