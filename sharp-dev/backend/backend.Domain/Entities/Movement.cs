using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Entities
{
    public class Movement : TimeTrackableEntity<int>
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public double BalanceAfter { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CorrespondentId { get; set; }
        public ApplicationUser Correspondent { get; set; }
        public int WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }
    }
}
