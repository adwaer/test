using System;
using System.Collections.Generic;
using System.Linq;
using Fix.Domain;
using Fix.Infrastructure;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Fix.Business
{
	public class WebNodesService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly List<int> _jobs = new List<int>();

		public WebNodesService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public void ActivateJobs()
		{
			RecurringJob.AddOrUpdate(() => Run(), Cron.Minutely);
		}

		public void Run()
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				using (var dataset = (IDataSetUow) scope.ServiceProvider.GetRequiredService(typeof(IDataSetUow)))
				{
					var webNodes = dataset
						.Query<WebNode>()
						.ToArray();

					foreach (var job in _jobs.Where(jobId => webNodes.All(webNode => webNode.Id != jobId)))
					{
						RecurringJob.RemoveIfExists(job.ToString());
					}

					foreach (var webNode in webNodes)
					{
						RecurringJob.AddOrUpdate(webNode.Id.ToString(),
							() => new NodeCheckJob(_serviceProvider)
								.Run(webNode.Url, webNode.Id),
							Cron.MinuteInterval(webNode.Interval));

						if (!_jobs.Contains(webNode.Id))
						{
							_jobs.Add(webNode.Id);
						}
					}
				}
			}
		}
	}
}