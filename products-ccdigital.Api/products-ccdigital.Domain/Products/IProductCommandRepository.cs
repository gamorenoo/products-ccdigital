
namespace products_ccdigital.Domain.Products
{
    public interface IProductCommandRepository
    {
        /// <summary>
        ///  Crear Producto
        /// </summary>
        /// <returns></returns>
        Task<Product> CreateAsync(Product product);

        /// <summary>
        ///  Actualizar Producto
        /// </summary>
        /// <returns></returns>
        Task<Product> UpdateAsync(Product product);

        /// <summary>
        /// Eliminar Producto
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync(Product product);
    }
}
