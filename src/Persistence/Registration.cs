using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Persistence.Contexts;
using Bcan.Backend.Persistence.Repositories;
using Bcan.Backend.Persistence.Options;
using Microsoft.EntityFrameworkCore;

namespace Bcan.Backend.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlDbOptions = configuration.GetSection(SqlDbOptions.Key).Get<SqlDbOptions>();
            
            // Use local sql server if it is dev.
            if(!string.IsNullOrWhiteSpace(sqlDbOptions.AppDbConnection))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(sqlDbOptions.AppDbConnection));
            }
            
                    

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}