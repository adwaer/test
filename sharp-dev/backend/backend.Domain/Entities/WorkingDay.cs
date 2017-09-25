using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using In.Entity;

namespace backend.Domain.Entities
{
    public class WorkingDay : DefaultEntity
    {
        [Required]
        public double Balance { get; set; }
        [Required]
        [Index("UX_USER_DATE", 1, IsUnique = true)]
        public DateTime Date { get; set; }
        [Index("UX_USER_DATE", 2, IsUnique = true), Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
    }
}
