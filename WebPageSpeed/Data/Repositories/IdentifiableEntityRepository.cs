using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories
{
    public abstract class IdentifiableEntityRepository<TEntity>
        : BaseRepository<TEntity>, IIdentifiableEntityRepository<TEntity>
        where TEntity : IdentifiableEntity, new()
    {
        protected IdentifiableEntityRepository(WebPageSpeedContext context) 
            : base(context)
        {
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await GetAll().SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}