using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers
{
    public class DeleteShoppingCartByUserNameHandler : IRequestHandler<DeleteShoppingCartByUserNameCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteShoppingCartByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<bool> Handle(DeleteShoppingCartByUserNameCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteBasket(request.username);
        }
    }
}
