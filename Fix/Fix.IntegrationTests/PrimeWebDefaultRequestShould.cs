using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Fix.WebApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace Fix.IntegrationTests
{
	public class PrimeWebDefaultRequestShould
	{
		private readonly HttpClient _client;

		public PrimeWebDefaultRequestShould()
		{
			var webHostBuilder = new WebHostBuilder()
				.UseEnvironment("Development")
				.UseContentRoot(CalculateRelativeContentRootPath())
				.UseConfiguration(new ConfigurationBuilder()
					.SetBasePath(CalculateRelativeContentRootPath())
					.AddJsonFile("appsettings.json")
					.Build()
				)
				.UseStartup<Startup>();

			var testServer = new TestServer(webHostBuilder);
			_client = testServer.CreateClient();
		}

		[Fact]
		public async Task Test1()
		{
			// Act
			var response = await _client.GetAsync("/");
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			// Assert
			Assert.Contains("Home Page", responseString);
		}
		
		private string CalculateRelativeContentRootPath() => 
			Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, 
				@"..\..\..\..\Fix.WebApp");       
	}
}