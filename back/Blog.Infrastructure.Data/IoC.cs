using Blog.Infrastructure.Data.Interfaces;
using Blog.SharedKernel.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Infrastructure.Data
{
    public static class IoC
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<DbSession>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.RegisterRepositories(Assembly.GetExecutingAssembly());
        }
    }
}
