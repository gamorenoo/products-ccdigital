using Microsoft.EntityFrameworkCore;
using products_ccdigital.Domain.Products;
using products_ccdigital.Infrastructure.Repositories.GenericRepository.QueryRepository;

namespace products_ccdigital.Infrastructure.Repositories.ProductRepository
{
    internal class ProductQueryRepository : IProductQueryRepository
    {
        private readonly IQueryRepository<Product> _productQueryyRepository;

        public ProductQueryRepository(IQueryRepository<Product> productQueryRepository)
        {
            _productQueryyRepository = productQueryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productQueryyRepository.GetList();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            var prperties = await _productQueryyRepository.GetList(x => x.Id == id);

            return prperties.FirstOrDefault();
        }

        public async Task<IEnumerable<Product>> GetPaginated(int page, int pageSize)
        {
            return await _productQueryyRepository.GetAll().Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
