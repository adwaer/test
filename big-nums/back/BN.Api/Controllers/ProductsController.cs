using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BN.Api.Models;
using BN.Api.Models.Responses;
using BN.Domain.Features.Products;
using BN.Domain.Features.Products.Commands;
using BN.Domain.Features.Products.Models;
using In.Cqrs.Command;
using In.Cqrs.Query.Queries;
using In.FunctionalCSharp;
using In.Web.Middlerwares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BN.Api.Controllers
{
    /// <summary>
    /// Controller API for products
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="messageSender"></param>
        public ProductsController(IQueryBuilder queryBuilder, IMessageSender messageSender)
        {
            _queryBuilder = queryBuilder;
            _messageSender = messageSender;
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MiddlewareExceptionWrapper))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MiddlewareExceptionWrapper))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var spec = ProductSpecifications.WithId(id)
                       & ProductSpecifications.NotDeleted();

            var product = await _queryBuilder
                .ForGeneric<Product>()
                .Where(spec)
                .ProjectTo<ProductViewModel>()
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductSearchResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MiddlewareExceptionWrapper))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MiddlewareExceptionWrapper))]
        public async Task<ActionResult> GetAll([FromQuery] ProductSearchRequest request)
        {
            if (request == null)
            {
                throw new InvalidOperationException("The search model is invalid");
            }

            var spec = ProductSpecifications.NotDeleted();
            if (!string.IsNullOrEmpty(request.Search))
            {
                spec &= ProductSpecifications.IsContains(request.Search);
            }

            var query = _queryBuilder
                .ForGeneric<Product>()
                .Where(spec);

            if (string.IsNullOrEmpty(request.OrderBy))
            {
                query = query.OrderBy(product => product.LastModified, true);
            }
            else if (string.Equals(request.OrderBy, nameof(Product.Name), StringComparison.InvariantCultureIgnoreCase))
            {
                query = query.OrderBy(product => product.Name, request.IsDesc);
            }
            else if (string.Equals(request.OrderBy, nameof(Product.Price), StringComparison.InvariantCultureIgnoreCase))
            {
                query = query.OrderBy(product => product.Price, request.IsDesc);
            }
            else
            {
                throw new InvalidOperationException("The order field is not correct");
            }

            var products = await query
                .ProjectTo<ProductViewModel>()
                .PagedAsync(request.Page, request.PageSize);

            return Ok(new ProductSearchResponse
            {
                Data = products.ToArray(),
                Count = products.TotalItemCount
            });
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MiddlewareExceptionWrapper))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MiddlewareExceptionWrapper))]
        public async Task<ActionResult> Create(ProductEditRequestModel request)
        {
            return await CreateProductCmd.Create(request.Name, request.Price)
                .Bind(async cmd => await _messageSender.SendAsync<CreateProductCmd, int>(cmd))
                .Finally(result =>
                    result.IsSuccess
                        ? CreatedAtAction(nameof(GetById), new {id = result.Value}, null)
                        : throw new InvalidOperationException(result.Error)
                );
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MiddlewareExceptionWrapper))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MiddlewareExceptionWrapper))]
        public async Task<ActionResult> Update(int id, ProductEditRequestModel request)
        {
            return await EditProductCmd.Create(id, request.Name, request.Price)
                .Bind(async cmd => await _messageSender.SendAsync(cmd))
                .Finally(result => result.IsSuccess ? NoContent() : throw new InvalidOperationException(result.Error));
        }

        /// <summary>
        /// Delete existing product
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MiddlewareExceptionWrapper))]
        public async Task<ActionResult> Delete(int id)
        {
            return await DeleteProductCmd.Create(id)
                .Bind(async cmd => await _messageSender.SendAsync(cmd))
                .Finally(result => result.IsSuccess ? NoContent() : throw new InvalidOperationException(result.Error));
        }
    }
}