using Microsoft.Extensions.DependencyInjection;

namespace DeloUltimate.Presentation.ViewModels.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddTransient<MainWindowViewModel>()
            .AddTransient<HomeViewModel>()
            .AddTransient<AccountActivityViewModel>()
            .AddTransient<AccountAttachmentViewModel>()
            .AddTransient<CabinetAttachmentViewModel>()
            .AddTransient<EmployeeChangesViewModel>()
            .AddTransient<InvalidPersonViewModel>();
    }
}
