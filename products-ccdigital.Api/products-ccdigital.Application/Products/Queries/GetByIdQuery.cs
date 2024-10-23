using MediatR;
using AutoMapper;
using products_ccdigital.Domain.Products;

namespace products_ccdigital.Application.Products.Queries
{
    public class GetByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductDto>
    {
        private IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<ProductDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetByIdAsync(request.Id);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
