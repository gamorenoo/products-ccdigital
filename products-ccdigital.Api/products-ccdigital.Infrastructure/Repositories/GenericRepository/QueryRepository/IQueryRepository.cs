using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace products_ccdigital.Infrastructure.Repositories.GenericRepository.QueryRepository
{
    /// <summary>
    /// Generic interfaz for query repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T> where T : class, new()
    {
        /// <summary>
        /// Get One record applying filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Get records applying filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null);

        IQueryable<T> GetAll();

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
