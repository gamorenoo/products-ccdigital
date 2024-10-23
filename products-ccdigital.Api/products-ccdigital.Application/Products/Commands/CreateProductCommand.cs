using MediatR;
using AutoMapper;
using products_ccdigital.Domain.Products;

namespace products_ccdigital.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public ProductCreateDto Product { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IMapper _mapper;
        private IProductCommandRepository _productCommandRepository;

        public CreateProductCommandHandler(IProductCommandRepository productCommandRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productCommandRepository = productCommandRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Product.Name,
                Code = request.Product.Code,
                Description = request.Product.Description,
                Price = request.Product.Price,
                Stock = request.Product.Stock,
            };

            product = await _productCommandRepository.CreateAsync(product);

            return _mapper.Map<ProductDto>(product);

        }
    }
}
