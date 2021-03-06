﻿using Microsoft.EntityFrameworkCore;
using WebPageSpeed.Models;

namespace WebPageSpeed.Data
{
    public class WebPageSpeedContext : DbContext
    {
        public DbSet<WebSite> WebSites { get; set; }
        public DbSet<WebPage> WebPages { get; set; }

        public WebPageSpeedContext(DbContextOptions options) 
            : base(options)
        {
        }
    }
}