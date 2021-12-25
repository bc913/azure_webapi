using Microsoft.Extensions.DependencyInjection;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Persistence.Repositories;

namespace Bcan.Backend.Persistence
{
    public static class Registration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}