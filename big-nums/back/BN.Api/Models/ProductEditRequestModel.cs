namespace BN.Api.Models
{
    /// <summary>
    /// Product view model
    /// </summary>
    public class ProductEditRequestModel
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        public decimal Price { get; set; }        
    }
}