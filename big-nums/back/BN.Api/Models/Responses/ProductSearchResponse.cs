namespace BN.Api.Models.Responses
{
    /// <summary>
    /// Product search response
    /// </summary>
    public class ProductSearchResponse
    {
        /// <summary>
        /// Data
        /// </summary>
        public ProductViewModel[] Data { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get; set; }
    }
}