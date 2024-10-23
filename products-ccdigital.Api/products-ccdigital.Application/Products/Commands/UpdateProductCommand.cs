using AutoMapper;
using MediatR;
using products_ccdigital.Application.Common.Exceptions;
using products_ccdigital.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace products_ccdigital.Application.Products.Commands
{
    public class UpdateProductCommand: IRequest<ProductDto>
    {
        public ProductDto Product { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private IProductCommandRepository _productCommandRepository;
        private IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductCommandRepository productCommandRepository,
            IProductQueryRepository productQueryRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetByIdAsync(request.Product.Id.Value);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.Product.Id);
            }

            product.Name = request.Product.Name;
            product.Code = request.Product.Code;
            product.Description = request.Product.Description;
            product.Price = request.Product.Price;
            product.Stock = request.Product.Stock;

            product = await _productCommandRepository.UpdateAsync(product);

            return _mapper.Map<ProductDto>(product);

        }
    }
}
