using DeloUltimate.Domain.Entities;
using DeloUltimate.Eos.AppServices.CompareServices;
using DeloUltimate.Eos.AppServices.DataServices;
using DeloUltimate.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeloUltimate.Eos.AppServices
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddImportServices()
            .AddExportServices()
            .AddCompareServices();

        private static IServiceCollection AddImportServices(this IServiceCollection services) => services
            .AddScoped<IDataImportService<Account>, DataImportService<Account>>()
            .AddScoped<IDataImportService<AccountActivity>, DataImportService<AccountActivity>>()
            .AddScoped<IDataImportService<Cabinet>, DataImportService<Cabinet>>()
            .AddScoped<IDataImportService<Employee>, DataImportService<Employee>>()
            .AddScoped<IDataImportService<InvalidPerson>, DataImportService<InvalidPerson>>();

        private static IServiceCollection AddExportServices(this IServiceCollection services) => services
            .AddScoped<IDataExportService<Employee>, DataExportService<Employee>>();

        public static IServiceCollection AddCompareServices(this IServiceCollection services) => services
            .AddScoped<ICompareService<Employee>, PersonCompareService>();
    }
}
