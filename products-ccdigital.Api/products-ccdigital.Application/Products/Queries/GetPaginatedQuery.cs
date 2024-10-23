using MediatR;
using AutoMapper;
using products_ccdigital.Domain.Common;
using products_ccdigital.Domain.Products;

namespace products_ccdigital.Application.Products.Queries
{
    public class GetPaginatedQuery : IRequest<PaginatedResult<ProductDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetPaginatedQueryHandler : IRequestHandler<GetPaginatedQuery, PaginatedResult<ProductDto>>
    {
        private IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetPaginatedQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetPaginatedQuery request, CancellationToken cancellationToken)
        {
            var listPrducts = await _productQueryRepository.GetPaginated(request.Page, request.PageSize);

            var listProductsDto = _mapper.Map<IEnumerable<ProductDto>>(listPrducts);

            return new PaginatedResult<ProductDto>
            {
                Data = listProductsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                Count = listProductsDto.Count()
            };
        }
    }
}
