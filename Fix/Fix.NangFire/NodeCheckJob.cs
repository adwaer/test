using System;
using System.Linq;
using Fix.Domain;
using Fix.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.NangFire
{
	public class NodeCheckJob
	{
		private readonly IServiceProvider _serviceProvider;

		public NodeCheckJob(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public void Run(string url, int id)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				using (var dataset = (IDataSetUow) scope.ServiceProvider.GetService(typeof(IDataSetUow)))
				{
					var webNode = dataset
						.Query<WebNode>()
						.SingleOrDefault(node => node.Id == id);

					if (webNode == null)
					{
						return;
					}

					var isOk = IsOk(url);
					webNode.IsAvailable = isOk;
					dataset.FixupState(webNode);

					dataset.AddEntity(new WebNodeStatusHistory
					{
						Date = DateTime.UtcNow,
						IsAvailable = isOk,
						NodeId = id
					});
					dataset.Commit();
				}
			}
		}

		private static bool IsOk(string url)
		{
			using (var client = new MyClient())
			{
				client.HeadOnly = true;
				try
				{
					client.DownloadString(new Uri(url));
				}
				catch
				{
					return false;
				}

				return true;
			}
		}
	}
}