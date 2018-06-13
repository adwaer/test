using System.ComponentModel.DataAnnotations;
using M13.InterviewProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace M13.InterviewProject.Controllers
{
    /// <summary>
    /// sample usage:
    /// 1) check xpath rule '//ol' for site yandex.ru: http://localhost:56660/api/rules/add?site=yandex.ru&rule=%2f%2fol
    /// 2) check rule is saved:  http://localhost:56660/api/rules/get?site=yandex.ru
    /// 3) view text parsed by rule: http://localhost:56660/api/rules/test?page=yandex.ru
    /// </summary>
    [Route("api/[controller]")]
    public class RulesController : Controller
    {
        private readonly RulesService _rulesService;

        public RulesController(RulesService rulesService)
        {
            _rulesService = rulesService;
        }


        /// <summary>
        /// Post request for for set site rule
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rule"></param>
        [HttpPost]
        public IActionResult Post([Required]string id, [Required]string rule)
        {
            _rulesService.Set(id, rule);
            return Ok();
        }

        /// <summary>
        /// Get request for access to site rule
        /// </summary>
        /// <param name="id">Site</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string id)
        {
            var rule = _rulesService.Get(id);
            if (!string.IsNullOrEmpty(rule))
            {
                return Ok(new
                {
                    Rules = rule
                });
            }

            return NotFound();

        }

        /// <summary>
        /// Delete Method for site rules remove
        /// </summary>
        /// <param name="id">Site</param>
        [HttpDelete]
        public void Delete(string id)
        {
            _rulesService.Delete(id);
        }

    }

}
