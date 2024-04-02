using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediatr;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
        {
            _mediatr = mediator;
            _logger = logger;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var rs = await _mediatr.Send(new CreateDiscountCommand(request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount));
            _logger.LogInformation($"Discount is created for the Product Name: {request.Coupon.ProductName}, Amount: {request.Coupon.Amount}");
            return rs;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var rs = await _mediatr.Send(new GetDiscountQuery(request.ProductName));
            _logger.LogInformation($"Discount is retrieved for the Product Name: {rs.ProductName}, Amount: {rs.Amount}");
            return rs;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var rs = await _mediatr.Send(new UpdateDiscountCommand(new Core.Entities.Coupon
            {
                Id = request.Coupon.Id,
                ProductName = request.Coupon.ProductName,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description
            }));
            _logger.LogInformation($"Discount is updated for the Product Name: {request.Coupon.ProductName}, Amount: {request.Coupon.Amount}");
            return rs;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var rs = await _mediatr.Send(new DeleteDiscountCommand(request.ProductName));
            _logger.LogInformation($"Discount is deleted for the Product Name: {request.ProductName}");
            return new DeleteDiscountResponse { Success = rs };
        }
    }
}
