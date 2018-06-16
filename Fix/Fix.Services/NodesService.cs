using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Fix.Domain;
using Fix.Infrastructure;
using Fix.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Fix.Services
{
	public class NodesService : INodesService
	{
		private readonly IDataSetUow _dataSetUow;
		private readonly IMapper _mapper;

		public NodesService(IDataSetUow dataSetUow, IMapper mapper)
		{
			_dataSetUow = dataSetUow;
			_mapper = mapper;
		}

		public async Task<TViewModel[]> GetAll<TViewModel>()
		{
			return await _dataSetUow
				.Query<WebNode>()
				.ProjectTo<TViewModel>()
				.ToArrayAsync();
		}

		public async Task<TViewModel> GetById<TViewModel>(int id)
		{
			return await _dataSetUow
				.Query<WebNode>()
				.Where(webNode => webNode.Id == id)
				.ProjectTo<TViewModel>()
				.SingleAsync();
		}

		public async Task<int> Create<TViewModel>(TViewModel model)
		{
			var webNode = _mapper.Map<WebNode>(model);

			_dataSetUow.AddEntity(webNode);
			return await _dataSetUow.CommitAsync();
		}
		
		public async Task<int> Edit<TViewModel>(int id, TViewModel model)
		{
			var webNode = _mapper.Map<WebNode>(model);
			webNode.Id = id;

			_dataSetUow.AddEntity(webNode);
			_dataSetUow.FixupState(webNode);

			return await _dataSetUow.CommitAsync();
		}
		
		public async Task<int> Delete(int id)
		{
			var node = await _dataSetUow
				.FindAsync<WebNode>(id);

			_dataSetUow.RemoveEntity(node);
			return await _dataSetUow.CommitAsync();
		}
	}
}