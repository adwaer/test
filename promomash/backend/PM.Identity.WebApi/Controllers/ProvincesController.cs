using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Domain.CountriesContext;
using PM.Identity.WebApi.Models;
using PM.Models;
using SilentNotary.Cqrs.Queries;

namespace PM.Identity.WebApi.Controllers
{
    /// <summary>
    /// provinces api
    /// </summary>
    [Route("api/Countries/{countryId}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProvincesController : ControllerBase
    {
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ProvincesController(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        /// <summary>
        /// Get provinces
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 30)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProvinceViewModel>))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> Get(int countryId)
        {
            // Let's not be mad, I understand that this is not SRP, need move it to query handler, but let's save time for the test task
            var result = await _queryBuilder
                .Generic<Province>()
                .Where(ProvinceSpecifications.WithCountryId(countryId))
                .OrderBy(c => c.Name)
                .ProjectTo<ProvinceViewModel>()
                .AllAsync();

            return Ok(result);
        }
    }
}