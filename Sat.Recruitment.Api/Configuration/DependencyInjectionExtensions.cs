using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sat.Domain.Services;
using Sat.Application.Services;
using Sat.Domain.Repositories;
using Sat.PersistenseFile.Repositories;

namespace Sat.Recruitment.Api.Configuration
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
