using System.Runtime.Serialization;
using In.Cqrs;

namespace backend.Domain
{
    [DataContract]
    public class AddressBook : EntityBase
    {
        [DataMember]
        public string Fio { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Division { get; set; }
        [DataMember]
        public string Position { get; set; }
    }
}
