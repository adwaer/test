using System.Linq;
using System.Threading.Tasks;
using Fix.Infrastructure.Services;
using Fix.WebApp.Controllers;
using Fix.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Fix.Tests
{
	public class HomeControllerTests
	{
		[Fact]
		public async Task Index_ReturnsAViewResult_WithAListOfNodes()
		{
			var mockSvc = new Mock<INodesService>();
			mockSvc
				.Setup(svc => svc.GetAll<WebNodeViewModel>())
				.Returns(Task.FromResult(_nodes));

			var controller = new HomeController(mockSvc.Object);
			var result = await controller.Index();

			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<WebNodeViewModel[]>(
				viewResult.ViewData.Model);

			Assert.Equal(_nodes.Length, model.Count());
		}

		private readonly WebNodeViewModel[] _nodes =
		{
			new WebNodeViewModel
			{
				IsAvailable = true,
				Name = "google",
				Url = "https://google.com"
			},
			new WebNodeViewModel
			{
				IsAvailable = false,
				Name = "yandex",
				Url = "https://ya.ru"
			}
		};
	}
}