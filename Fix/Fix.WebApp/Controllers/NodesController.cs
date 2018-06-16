using System;
using System.Threading.Tasks;
using Fix.Infrastructure.Services;
using Fix.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fix.WebApp.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class NodesController : Controller
	{
		private readonly INodesService _nodesService;

		public NodesController(INodesService nodesService)
		{
			_nodesService = nodesService;
		}

		// GET: Nodes
		public async Task<IActionResult> Index()
		{
			return View(await _nodesService.GetAll<WebNodeEditViewModel>());
		}

		// GET: Nodes/Details/5
		public async Task<ActionResult> Details(int id)
		{
			return View(await _nodesService.GetById<WebNodeEditViewModel>(id));
		}

		public ActionResult Create()
		{
			return View();
		}

		// POST: Nodes/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(WebNodeEditViewModel model)
		{
			try
			{
				await _nodesService.Create(model);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return View();
			}
		}

		// GET: Nodes/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			return View(await _nodesService.GetById<WebNodeEditViewModel>(id));
		}

		// POST: Nodes/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, WebNodeEditViewModel model)
		{
			try
			{
				await _nodesService.Edit(id, model);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return View();
			}
		}

		// GET: Nodes/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			return View(await _nodesService.GetById<WebNodeEditViewModel>(id));
		}

		// POST: Nodes/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			try
			{
				await _nodesService.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return await Delete(id);
			}
		}
	}
}