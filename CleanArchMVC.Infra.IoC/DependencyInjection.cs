using CleanArchMVC.Domain.Interface;
using CleanArchMVC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchMVC.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrasctructure(
            this IServiceCollection services,
            IConfiguration configuration) 
        { 
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoryRepository, ICategoryRepository>();
            services.AddScoped<IProductRepository, IProductRepository>();

            return services;
        }
    }
}
