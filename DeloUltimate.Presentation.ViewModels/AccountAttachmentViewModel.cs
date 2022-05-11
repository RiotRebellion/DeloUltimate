using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;

namespace DeloUltimate.Presentation.ViewModels
{
    public class AccountAttachmentViewModel : ViewModel
    {
        public AccountAttachmentViewModel(IDataImportService<Account> service)
        {
            AccountAttachmentCollection = new ObservableCollection<Account>(service.ImportFromDatabase());
        }

        #region Properties

        #region AccountAttachmentCollection

        private ObservableCollection<Account> _accountAttachmentCollection;

        public ObservableCollection<Account> AccountAttachmentCollection
        {
            get => _accountAttachmentCollection;
            set => Set(ref _accountAttachmentCollection, value);
        }

        #endregion

        #endregion
    }
}
