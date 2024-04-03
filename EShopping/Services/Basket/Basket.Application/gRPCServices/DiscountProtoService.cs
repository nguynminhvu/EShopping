using Discount.Grpc.Protos;

namespace Basket.Application.gRPCServices
{
    public class DiscountProtoService
    {
        private readonly Discount.Grpc.Protos.DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

        public DiscountProtoService(Discount.Grpc.Protos.DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
        {
            _discountProtoServiceClient = discountProtoServiceClient;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoServiceClient.GetDiscountAsync(discountRequest);
        }
    }
}
