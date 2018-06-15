using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fix.Domain;
using Fix.Infrastructure;
using Fix.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fix.WebApp.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class NodesController : Controller
	{
		private readonly IDataSetUow _dataSetUow;
		private readonly IMapper _mapper;

		public NodesController(IDataSetUow dataSetUow, IMapper mapper)
		{
			_dataSetUow = dataSetUow;
			_mapper = mapper;
		}

		// GET: Nodes
		public async Task<IActionResult> Index()
		{
			var nodes = await _dataSetUow
				.Query<WebNode>()
				.ProjectTo<WebNodeEditViewModel>()
				.ToArrayAsync();

			return View(nodes);
		}

		// GET: Nodes/Details/5
		public async Task<ActionResult> Details(int id)
		{
			var node = await _dataSetUow
				.Query<WebNode>()
				.Where(webNode => webNode.Id == id)
				.ProjectTo<WebNodeEditViewModel>()
				.SingleAsync();

			return View(node);
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
				var webNode = _mapper.Map<WebNode>(model);
				_dataSetUow.AddEntity(webNode);
				await _dataSetUow.CommitAsync();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return View();
			}
		}

		// GET: Nodes/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			var node = await _dataSetUow
				.Query<WebNode>()
				.Where(webNode => webNode.Id == id)
				.ProjectTo<WebNodeEditViewModel>()
				.SingleAsync();

			return View(node);
		}

		// POST: Nodes/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, WebNodeEditViewModel model)
		{
			try
			{
				var webNode = _mapper.Map<WebNode>(model);
				webNode.Id = id;

				_dataSetUow.AddEntity(webNode);
				_dataSetUow.FixupState(webNode);

				await _dataSetUow.CommitAsync();

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
			var node = await _dataSetUow
				.Query<WebNode>()
				.Where(webNode => webNode.Id == id)
				.ProjectTo<WebNodeEditViewModel>()
				.SingleAsync();

			return View(node);
		}

		// POST: Nodes/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			try
			{
				var node = await _dataSetUow
					.FindAsync<WebNode>(id);
				
				_dataSetUow.RemoveEntity(node);
				await _dataSetUow.CommitAsync();

				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return View();
			}
		}
	}
}