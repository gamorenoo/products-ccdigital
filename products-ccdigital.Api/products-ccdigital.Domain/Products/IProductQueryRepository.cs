
namespace products_ccdigital.Domain.Products
{
    public interface IProductQueryRepository
    {
        /// <summary>
        /// Obtener la lista de productos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllAsync();

        /// <summary>
        /// Obtener un producto por su Identificador único
        /// </summary>
        /// <returns></returns>
        Task<Product?> GetByIdAsync(Guid id);

        /// <summary>
        /// Obtener la lista de productos paginados
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetPaginated(int page, int pageSize);
    }
}
