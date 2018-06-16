using System.Diagnostics;
using System.Threading.Tasks;
using Fix.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Fix.WebApp.Models;

namespace Fix.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly INodesService _nodesService;

		public HomeController(INodesService nodesService)
		{
			_nodesService = nodesService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _nodesService.GetAll<WebNodeViewModel>());
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}