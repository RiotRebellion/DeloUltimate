using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;

namespace DeloUltimate.Presentation.ViewModels
{
    public class AccountActivityViewModel : ViewModel
    {
        private IDataImportService<AccountActivity> _service;

        public AccountActivityViewModel(IDataImportService<AccountActivity> service)
        {
            _service = service;
            AccountActivityCollection = new ObservableCollection<AccountActivity>(service.ImportFromDatabase());
        }

        #region Properties

        #region AccountActivityCollection

        private ObservableCollection<AccountActivity> _accountActivityCollection;

        public ObservableCollection<AccountActivity> AccountActivityCollection
        {
            get => _accountActivityCollection;
            set => Set(ref _accountActivityCollection, value);
        }

        #endregion

        #endregion
    }
}
