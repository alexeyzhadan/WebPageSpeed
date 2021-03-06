﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebPageSpeed.Data.Repositories.Interface;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data.Repositories
{
    public class WebSiteRepository : IdentifiableEntityRepository<WebSite>, IWebSiteRepository
    {
        public WebSiteRepository(WebPageSpeedContext context)
            : base(context)
        {
        }

        public async Task<WebSite> GetByIdWithWebPagesAsync(long id)
        {
            var webSite = await GetAll()
                .Include(w => w.WebPages)
                .SingleOrDefaultAsync(w => w.Id == id);
            return webSite;
        }

        public IQueryable<WebSite> GetAllOrderedByDateDesc(int skip, int take)
        {
            return GetAll()
                .Skip(skip)
                .Take(take)
                .OrderByDescending(w => w.DateOfAnalysis);
        }

        public async Task<int> GetCountAsync()
        {
            return await GetAll().CountAsync();
        }
    }
}