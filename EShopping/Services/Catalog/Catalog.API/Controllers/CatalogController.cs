using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    public class CatalogController : ApiController
    {
        private readonly IMediator _mediatr;

        public CatalogController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct(string id)
        {
            var query = new GetProductByIdQuery(id);
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }

        [HttpGet]
        [Route("[action]/productName", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var query = new GetProductsByNameQuery(name);
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }


        [HttpGet]
        [Route("GetAllProduct")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProduct([FromQuery]CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductsQuery(catalogSpecParams);
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }


        [HttpGet]
        [Route("GetAllBrand")]
        [ProducesResponseType(typeof(IList<BrandResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBrands()
        {
            var query = new GetAllBrandsQuery();
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }

        [HttpGet]
        [Route("GetAllType")]
        [ProducesResponseType(typeof(IList<Type>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTypes()
        {
            var query = new GetAllTypesQuery();
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }

        [HttpGet]
        [Route("[action]/brand", Name = "GetProductsByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductsByBrandName(string brand)
        {
            var query = new GetProductsByBrandNameQuery(brand);
            var rs = await _mediatr.Send(query);
            return Ok(rs);
        }


        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            var rs = await _mediatr.Send(createProductCommand);
            return StatusCode(StatusCodes.Status201Created, rs);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProductCommand)
        {
            var rs = await _mediatr.Send(updateProductCommand);
            return rs ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var rs = await _mediatr.Send(new DeleteProductCommand(id));
            return rs ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
