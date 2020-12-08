using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebPageSpeed.Data.Extensions
{
    public static class EnsureMigrationExtension
    {
        public static void EnsureMigrationOfContext(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<WebPageSpeedContext>();
                context.Database.Migrate();
            }
        }
    }
}