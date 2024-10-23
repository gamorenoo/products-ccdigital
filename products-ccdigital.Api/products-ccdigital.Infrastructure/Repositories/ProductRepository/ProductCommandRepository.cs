using products_ccdigital.Domain.Products;
using products_ccdigital.Infrastructure.Repositories.GenericRepository.CommandRepository;

namespace products_ccdigital.Infrastructure.Repositories.ProductRepository
{
    public class ProductCommandRepository: IProductCommandRepository
    {
        private readonly ICommandRepository<Product> _productCommandRepository;

        public ProductCommandRepository(ICommandRepository<Product> productCommandRepository)
        {
            _productCommandRepository = productCommandRepository;
        }

        /// <inheritdoc/>
        public async Task<Product> CreateAsync(Product product)
        {
            return await _productCommandRepository.Add(product);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(Product product)
        {
            await _productCommandRepository.Delete(product);
        }

        /// <inheritdoc/>
        public async Task<Product> UpdateAsync(Product product)
        {
            return await _productCommandRepository.Update(product);
        }
    }
}
