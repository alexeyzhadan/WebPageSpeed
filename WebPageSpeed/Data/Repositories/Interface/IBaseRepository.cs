using System.Linq;
using System.Threading.Tasks;

namespace WebPageSpeed.Data.Repositories.Interface
{
    public interface IBaseRepository<TEntity> 
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(params TEntity[] entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}