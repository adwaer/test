using System.ComponentModel.DataAnnotations;

namespace Fix.WebApp.Models
{
	public class WebNodeEditViewModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
		
		[Required]
		public string Url { get; set; }

		[Display(Name = nameof(Interval) + " (in min)")]
		[Required]
		[Range(1, int.MaxValue)]
		public int Interval { get; set; }

		public bool IsAvailable { get; set; }
	}
}