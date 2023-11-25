using StyleMate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StyleMate.Data
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class StyleMateContextExtension
    {
        // zie boek blz 551
        public static IServiceCollection AddStyleMateContext(this IServiceCollection services, string connectionstring = "")
        {
            services.AddDbContext<StyleMateDataContext>(options =>
            {
                options.UseMySql(connectionstring,
                    new MySqlServerVersion(ServerVersion.AutoDetect(connectionstring)), 
                    b => b.MigrationsAssembly("StyleMateManager.API")); // Provide a ServerVersion enum
            });
            return services;
        }
    }
}