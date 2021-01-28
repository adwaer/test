namespace BN.Api.Models.Responses
{
    /// <summary>
    /// Product view model
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        
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