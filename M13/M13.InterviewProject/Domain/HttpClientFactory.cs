using System.Net;
using System.Net.Http;

namespace M13.InterviewProject.Domain
{
    public static class HttpClientFactory
    {
        public static HttpClient CreateClient()
        {
            return new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate
            });
        }
    }
}