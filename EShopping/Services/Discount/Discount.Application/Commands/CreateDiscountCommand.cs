using Discount.Core.Entities;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateDiscountCommand : IRequest<CouponModel>
    {
        public CreateDiscountCommand(string productName, string description, int amount)
        {
            ProductName = productName;
            Description = description;
            Amount = amount;
        }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
