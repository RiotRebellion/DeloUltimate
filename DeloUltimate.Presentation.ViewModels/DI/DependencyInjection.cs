using Microsoft.Extensions.DependencyInjection;

namespace DeloUltimate.Presentation.ViewModels.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<HomeViewModel>()
            .AddSingleton<AccountActivityViewModel>()
            .AddSingleton<AccountAttachmentViewModel>()
            .AddSingleton<CabinetAttachmentViewModel>()
            .AddSingleton<EmployeeChangesViewModel>()
            .AddSingleton<InvalidPersonViewModel>();
    }
}
