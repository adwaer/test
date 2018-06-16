using System.Threading.Tasks;

namespace Fix.Infrastructure.Services
{
	public interface INodesService
	{
		Task<TViewModel[]> GetAll<TViewModel>();
		Task<TViewModel> GetById<TViewModel>(int id);
		Task<int> Create<TViewModel>(TViewModel webNode);
		Task<int> Edit<TViewModel>(int id, TViewModel model);
		Task<int> Delete(int id);
	}
}