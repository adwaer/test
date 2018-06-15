using System;

namespace Fix.WebApp.Models
{
	public class WebNodeViewModel
	{	public string Name { get; set; }
		public string Url { get; set; }
		public bool IsAvailable { get; set; }
		
		public bool HaveHistory { get; set; }
	}
}