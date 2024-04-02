using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponModel>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
        }
        public async Task<CouponModel> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            await _discountRepository.UpdateDiscount(request.Coupon);
            return _mapper.Map<CouponModel>(request.Coupon);
        }
    }
}
