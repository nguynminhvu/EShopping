using Basket.Application.gRPCServices;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly DiscountProtoService _discountProtoService;
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache distributedCache, DiscountProtoService discountProtoService)
        {
            _discountProtoService = discountProtoService;
            _cache = distributedCache;
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            await _cache.RemoveAsync(userName);
            return (await GetBasket(userName)) + "" == "";
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null!;
            var rs = await _cache.GetStringAsync(userName);
            return rs != null ? JsonConvert.DeserializeObject<ShoppingCart>(rs)! : null!;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            foreach (var item in shoppingCart.Items)
            {
                var coupon = await _discountProtoService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            await _cache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.UserName);
        }
    }
}
