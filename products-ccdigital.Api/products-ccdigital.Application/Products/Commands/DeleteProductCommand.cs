using MediatR;
using products_ccdigital.Domain.Products;
using products_ccdigital.Application.Common.Exceptions;

namespace products_ccdigital.Application.Products.Commands
{
    public class DeleteProductCommand: IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private IProductCommandRepository _productCommandRepository;
        private IProductQueryRepository _productQueryRepository;

        public DeleteProductCommandHandler(IProductCommandRepository productCommandRepository,
            IProductQueryRepository productQueryRepository)
        {
            _productCommandRepository = productCommandRepository;
            _productQueryRepository = productQueryRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            await _productCommandRepository.DeleteAsync(product);

            return true;
        }
    }
}
