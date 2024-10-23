using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using products_ccdigital.Application.Common.Interfaces;
using products_ccdigital.Domain.Products;
using products_ccdigital.Infrastructure.Persistence;
using products_ccdigital.Infrastructure.Repositories.GenericRepository.CommandRepository;
using products_ccdigital.Infrastructure.Repositories.GenericRepository.QueryRepository;
using products_ccdigital.Infrastructure.Repositories.ProductRepository;

namespace products_ccdigital.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("SqlServerConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddTransient(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
            services.AddScoped<IProductCommandRepository, ProductCommandRepository>();

            return services;
        }
    }
}
