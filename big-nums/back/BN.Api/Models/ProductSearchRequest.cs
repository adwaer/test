namespace BN.Api.Models
{
    /// <summary>
    /// Product search request
    /// </summary>
    public class ProductSearchRequest
    {
        /// <summary>
        /// Search term
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Order column
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Is direction desc
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// Page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public int PageSize { get; set; }
    }
}