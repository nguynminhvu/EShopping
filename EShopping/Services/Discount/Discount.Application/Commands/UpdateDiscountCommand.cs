using Discount.Core.Entities;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public record UpdateDiscountCommand (Coupon Coupon): IRequest<CouponModel>
    {
    }
}
