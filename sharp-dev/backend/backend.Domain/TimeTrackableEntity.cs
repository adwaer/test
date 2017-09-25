using System;
using System.Web;
using In.Entity;

namespace backend.Domain
{
    //todo: refactor
    public abstract class TimeTrackableEntity<TId> : EntityBase<TId>
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Identity { get; set; }

        public void Update()
        {
            if (CreateDate == default(DateTime))
            {
                CreateDate = DateTime.UtcNow;
            }
            UpdateDate = DateTime.UtcNow;
            Identity = HttpContext.Current?.User?.Identity?.Name;
        }
        
    }
}
