using System.Collections.Generic;
using Fix.Infrastructure.Domain;

namespace Fix.Domain
{
	/// <summary>
	/// Web nofe
	/// </summary>
	public class WebNode : HasKeyBase<int>
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public bool IsAvailable { get; set; }
		public int Interval { get; set; }
		public ICollection<WebNodeStatusHistory> Histories { get; set; }
	}
}