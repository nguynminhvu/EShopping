using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public record GetProductsByBrandNameQuery(string brandName) : IRequest<IList<ProductResponse>>
    {
    }
}
