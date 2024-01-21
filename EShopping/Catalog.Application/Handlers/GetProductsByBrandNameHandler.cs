using Amazon.Runtime.Internal;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductsByBrandNameHandler : IRequestHandler<GetProductsByBrandNameQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByBrandNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductsByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductByBrand(request.brandName);
            return ProductMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(products.ToList());
        }
    }
}
