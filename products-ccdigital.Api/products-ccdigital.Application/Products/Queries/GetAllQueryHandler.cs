using MediatR;
using AutoMapper;
using products_ccdigital.Domain.Products;

namespace products_ccdigital.Application.Products.Queries
{
    public class GetAllQuery : IRequest<IEnumerable<ProductDto>>
    {

    }
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<ProductDto>>
    {
        private IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var listProducts = await _productQueryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(listProducts);
        }
    }
}
