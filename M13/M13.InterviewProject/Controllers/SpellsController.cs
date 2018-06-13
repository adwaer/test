using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using M13.InterviewProject.Domain;
using M13.InterviewProject.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M13.InterviewProject.Controllers
{
    /// <summary>
    /// sample usage:
    /// 4) view errors in text: http://localhost:56660/api/spells/errors?page=yandex.ru
    /// 5) view errors count in text: http://localhost:56660/api/spells/errorscount?page=yandex.ru
    /// </summary>
    public class SpellsController : Controller
    {
        private readonly UrlNodeRequestService _requestService;
        private readonly RulesService _rulesService;

        public SpellsController(UrlNodeRequestService requestService, RulesService rulesService)
        {
            _requestService = requestService;
            _rulesService = rulesService;
        }

        /// <summary>
        /// action api get test endpoint
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        [HttpGet("Test")]
        public async Task<IActionResult> Test(string page, string rule = null)
        {
            rule = rule ?? _rulesService.Get(new Uri("http://" + page).Host);
            var selection = await _requestService.Fetch(page, rule);
            if (selection == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                selection
            });
        }

        /// <summary>
        /// Проверить текст страницы по заданному адресу и получить список слов с ошибками
        /// </summary>
        [HttpGet("errors")]
        public async Task<IActionResult> SpellErrors(string page)
        {
            var selection = await _requestService.Fetch(page, _rulesService.Get(new Uri("http://" + page).Host));
            if (selection == null)
            {
                return NotFound();
            }

            var result = await GetErrors(selection);

            var textErrors = new List<string>(result.Count);
            foreach (ISpellCheckError error in result)
            {
                textErrors.Add(error.Word);
            }

            return Ok(textErrors);
        }

        /// <summary>
        /// Проверить текст страницы по заданному адресу и получить количество слов с ошибками
        /// </summary>
        [HttpGet("errorscount")]
        public async Task<IActionResult> SpellErrorsCount(string page)
        {
            var selection = await _requestService.Fetch(page, _rulesService.Get(new Uri("http://" + page).Host));
            if (selection == null)
            {
                return NotFound();
            }
            var textErrors = await GetErrors(selection);

            return Ok(textErrors.Count);
        }

        #region MyRegion

        /// <summary>
        /// используем сервис яндекса для поиска орфографических ошибок в тексте
        /// сервис возвращает список слов, в которых допущены ошибки
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private async Task<List<ISpellCheckError>> GetErrors(string text)
        {
            //используем сервис яндекса для поиска орфографических ошибок в тексте
            //сервис возвращает список слов, в которых допущены ошибки
            var response = await HttpClientFactory.CreateClient()
                .GetAsync($"http://speller.yandex.net/services/spellservice.json/checkText?text={WebUtility.UrlEncode(text)}");

            var json = await response.Content.ReadAsStringAsync();

            var errs = JsonConvert.DeserializeObject<SpellerErrors[]>(json);
            List<ISpellCheckError> errList = new List<ISpellCheckError>(errs.Length);
            foreach (SpellerErrors t in errs)
            {
                errList.Add(t);
            }

            return errList;
        }

        #endregion

    }

}
