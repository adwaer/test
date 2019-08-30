using SilentNotary.Common.Entity;

namespace PM.Models
{
    public class Province : HasKeyBase<int>
    {
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}