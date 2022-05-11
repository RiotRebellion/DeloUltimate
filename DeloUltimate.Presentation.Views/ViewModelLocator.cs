using DeloUltimate.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DeloUltimate.Presentation.Views
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public HomeViewModel HomeViewModel => App.Services.GetRequiredService<HomeViewModel>();
        public AccountActivityViewModel AccountActivityViewModel => App.Services.GetRequiredService<AccountActivityViewModel>();
        public AccountAttachmentViewModel AccountAttachmentViewModel => App.Services.GetRequiredService<AccountAttachmentViewModel>();
        public CabinetAttachmentViewModel CabinetAttachmentViewModel => App.Services.GetRequiredService<CabinetAttachmentViewModel>();
        public EmployeeChangesViewModel EmployeeChangesViewModel => App.Services.GetRequiredService<EmployeeChangesViewModel>();
        public InvalidPersonViewModel InvalidPersonViewModel => App.Services.GetRequiredService<InvalidPersonViewModel>();
    }
}
