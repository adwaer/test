using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace backend.WebApi.ViewModels
{
    /// <summary>
    /// Movement model
    /// </summary>
    [DataContract]
    public class MovementViewModel
    {
        /// <summary>
        /// amount
        /// </summary>
        [DataMember, Required]
        public double Amount { get; set; }
        /// <summary>
        /// target email
        /// </summary>
        [DataMember, Required]
        public string TargetEmail { get; set; }
    }
}