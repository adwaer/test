using System.ComponentModel.DataAnnotations;
using In.Entity;

namespace backend.Domain.Entities
{
    public class Movement : DefaultEntity
    {
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string MakeUserId { get; set; }
        public ApplicationUser MakeUser { get; set; }
        public string WorkingDayId { get; set; }
        public WorkingDay WorkingDay { get; set; }
    }
}
