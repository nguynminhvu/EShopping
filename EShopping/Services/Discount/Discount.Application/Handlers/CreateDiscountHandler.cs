using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponModel>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;

        public CreateDiscountHandler(IDiscountRepository discountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _discountRepository = discountRepository;
        }
        public async Task<CouponModel> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _discountRepository.CreateDiscount(coupon);
            return _mapper.Map<CouponModel>(coupon);
        }
    }
}
