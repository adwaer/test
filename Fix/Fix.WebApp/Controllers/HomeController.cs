using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Fix.Domain;
using Fix.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Fix.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Fix.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDataSetUow _dataSetUow;

		public HomeController(IDataSetUow dataSetUow)
		{
			_dataSetUow = dataSetUow;
		}

		public async Task<IActionResult> Index()
		{
			var nodes = await _dataSetUow
				.Query<WebNode>()
				.ProjectTo<WebNodeViewModel>()
				.ToArrayAsync();

			return View(nodes);
		}

		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}