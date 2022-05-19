using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class AccountActivityViewModel : ViewModel
    {
        private IDataImportService<AccountActivity> _dataImportService;
        private IDataExportService<AccountActivity> _dataExportService;

        public AccountActivityViewModel(IDataImportService<AccountActivity> dataImportService,
                                        IDataExportService<AccountActivity> dataExportService)
        {
            _dataImportService = dataImportService;
            _dataExportService = dataExportService;

            #region Registration

            #region Commands

            FetchData = new RelayCommand(OnFetchDataExecuted, CanFetchDataExecute);

            #endregion

            #endregion
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

        #region Commands

        #region FetchData

        public ICommand FetchData { get; private set; }

        private bool CanFetchDataExecute(object p) => AccountActivityCollection is null;

        private void OnFetchDataExecuted(object p)
        {
            AccountActivityCollection = new ObservableCollection<AccountActivity>(_dataImportService.ImportFromDatabase());
        }

        #endregion

        #endregion
    }
}
