using DeloUltimate.Domain.Entities;
using DeloUltimate.Infrastructure.Data.Contexts;
using DeloUltimate.Infrastructure.Data.Repositories;
using DeloUltimate.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeloUltimate.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services) => services
            .AddTransient<IDeloDbContext, DeloDbContext>()
            .AddTransient<IMiraDbContext, MiraDbContext>()
            .AddRepositories();

        private static IServiceCollection AddRepositories(this IServiceCollection services) => services
            .AddTransient<IGenericRepository<Account>, AccountRepository>()
            .AddTransient<IGenericRepository<AccountActivity>, AccountActivityRepository>()
            .AddTransient<IGenericRepository<Cabinet>, CabinetRepository>()
            .AddTransient<IGenericRepository<Employee>, EmployeeRepository>()
            .AddTransient<IGenericRepository<InvalidPerson>, InvalidPersonRepository>();
    }
}
