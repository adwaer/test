using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Identity.WebApi.Models;
using PM.Models;
using SilentNotary.Cqrs.Queries;

namespace PM.Identity.WebApi.Controllers
{
    /// <summary>
    /// Countries api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountriesController : ControllerBase
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public CountriesController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryViewModel>))]
        public async Task<ActionResult> Get()
        {
            // Let's not be mad, I understand that this is not SRP, need move it to query handler, but let's save time for the test task
            var result = await _queryBuilder
                .Generic<Country>()
                .OrderBy(c => c.Name)
                .ProjectTo<CountryViewModel>()
                .AllAsync();

            return Ok(result);
        }
    }
}