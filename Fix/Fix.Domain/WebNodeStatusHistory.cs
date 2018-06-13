using System;
using Fix.Infrastructure.Domain;

namespace Fix.Domain
{
	/// <summary>
	/// Change status history
	/// </summary>
	public class WebNodeStatusHistory : HasKeyBase<int>
	{
		public bool IsAvailable { get; set; }
		public DateTime Date { get; set; }
		public WebNode Node { get; set; } 
	}
}
