using System.Collections.Generic;
using SilentNotary.Common.Entity;

namespace PM.Models
{
    public class Country : HasKeyBase<int>
    {
        public string Name { get; set; }
        public ICollection<Province> Provinces { get; set; }
    }
}