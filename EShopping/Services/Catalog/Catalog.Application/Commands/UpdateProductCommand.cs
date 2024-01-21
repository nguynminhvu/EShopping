using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Application.Commands
{
    public record UpdateProductCommand(Product product):IRequest<bool>
    {

    }
}
