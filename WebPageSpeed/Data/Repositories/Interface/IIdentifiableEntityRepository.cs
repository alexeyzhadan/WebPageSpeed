using System.Threading.Tasks;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories.Interface
{
    public interface IIdentifiableEntityRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : IdentifiableEntity       
    {
        Task<TEntity> GetByIdAsync(long id);
    }
}