using System;
using In.DataAccess.Entity;

namespace BN.Domain.Features.Products.Models
{
    public class Product : DateTrackableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? Deleted { get; set; }
    }
}