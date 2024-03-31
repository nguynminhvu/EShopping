using MediatR;

namespace Basket.Application.Commands
{
    public record DeleteShoppingCartByUserNameCommand(string username) : IRequest<bool>
    {
    }
}
