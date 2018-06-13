using System;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using M13.InterviewProject.Domain;

namespace M13.InterviewProject.Services
{
    public class UrlNodeRequestService
    {
        private readonly RulesService _rulesService;

        public UrlNodeRequestService(RulesService rulesService)
        {
            _rulesService = rulesService;
        }

        public async Task<string> Fetch(string page, string rule)
        {
            var response = await HttpClientFactory.CreateClient().GetAsync("http://" + page);
            var document = new HtmlDocument();
            document.LoadHtml(await response.Content.ReadAsStringAsync());

            rule = rule ?? _rulesService.Get(new Uri("http://" + page).Host);
            if (rule == null)
            {
                return null;
            }

            var htmlNodeCollection = document.DocumentNode.SelectNodes(rule);
            if (htmlNodeCollection == null)
            {
                return null;
            }

            var pageRulesBuilder = new StringBuilder();
            foreach (var node in htmlNodeCollection)
            {
                pageRulesBuilder.AppendLine(node.InnerText);
            }

            return pageRulesBuilder.ToString();
        }
    }
}
