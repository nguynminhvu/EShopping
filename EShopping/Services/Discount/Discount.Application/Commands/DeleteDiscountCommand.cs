using MediatR;

namespace Discount.Application.Commands
{
    public record DeleteDiscountCommand(string productName):IRequest<bool>
    {
    }
}
