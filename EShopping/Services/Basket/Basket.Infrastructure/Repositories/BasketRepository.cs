using Basket.Core.Entities;
using Basket.Core.Repositories;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task DeleteBasket(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetBasket(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
