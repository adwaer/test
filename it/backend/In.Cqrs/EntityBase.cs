using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace In.Cqrs
{
    [DataContract]
    public abstract class EntityBase : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public virtual int Id { get; set; }

        public bool IsNew()
        {
            return Id.Equals(default(int));
        }

        public object GetId()
        {
            return Id;
        }
    }
}
