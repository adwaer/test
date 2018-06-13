using System.ComponentModel.DataAnnotations;

namespace Fix.WebApp.Models
{
	public class WebNodeViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		
		[Required]
		public string Url { get; set; }

		[Display(Name = nameof(Interval) + " (in sec)")]
		[Required]
		[Range(10, int.MaxValue)]
		public int Interval { get; set; }

		public bool IsAvailable { get; set; }
	}
}