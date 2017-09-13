using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using In.Cqrs;

namespace backend.Domain
{
    [DataContract]
    public class AddressBook : EntityBase
    {
        [DataMember, Required]
        public string Fio { get; set; }
        [DataMember, Required]
        public string Phone { get; set; }
        [DataMember, Required]
        public string Email { get; set; }
        [DataMember, Required]
        public string Division { get; set; }
        [DataMember, Required]
        public string Position { get; set; }
    }
}
