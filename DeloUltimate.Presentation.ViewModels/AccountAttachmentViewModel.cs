using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using DeloUltimate.Presentation.ViewModels.Models;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class AccountAttachmentViewModel : ViewModel
    {
        #region Fields

        IDataImportService<Account> _dataImportService;

        #endregion

        public AccountAttachmentViewModel(IDataImportService<Account> importService)
        {
            _dataImportService = importService;

            #region Registration

            #region Commands

            FetchData = new RelayCommand(OnFetchDataExecuted, CanFetchDataExecute);

            #endregion

            #endregion
        }

        #region Properties

        #region IsDataFetched

        private bool _isDataFetched = false;

        public bool IsDataFetched
        {
            get => _isDataFetched;
            set => Set(ref _isDataFetched, value);
        }

        #endregion

        #region AccountCollection

        private ObservableCollection<Account> _accountCollection;

        public ObservableCollection<Account> AccountCollection
        {
            get => _accountCollection;
            set => Set(ref _accountCollection, value);
        }

        #endregion

        #region AccountCollectionView

        private ListCollectionView _accountCollectionView;

        public ListCollectionView AccountCollectionView
        {
            get => _accountCollectionView;
            set => Set(ref _accountCollectionView, value);
        }

        #endregion

        #region CabinetFilterOptions

        private AccountFilterOptions _accountFilterOptions = AccountFilterOptions.Cabinet;

        public AccountFilterOptions AccountFilterOptions
        {
            get => _accountFilterOptions;
            set => Set(ref _accountFilterOptions, value);
        }

        #endregion

        #region FilterText

        private string _filterText = string.Empty;

        public string FilterText
        {
            get => _filterText;
            set
            {
                Set(ref _filterText, value);
                AccountCollectionView.Filter = AccountFilter;
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand FetchData { get; private set; }

        private bool CanFetchDataExecute(object p) => IsDataFetched == false;

        private void OnFetchDataExecuted(object p)
        {
            AccountCollection = new ObservableCollection<Account>(_dataImportService.ImportFromDatabase());
            AccountCollectionView = new ListCollectionView(AccountCollection);
            IsDataFetched = true;
        }

        #endregion

        #region EventHandlers

        private bool AccountFilter(object obj)
        {
            Account account = obj as Account;
            switch (AccountFilterOptions)
            {
                case (AccountFilterOptions.Cabinet):
                    return string.IsNullOrEmpty(FilterText) ? true :
                        account.Cabinet.ToLower().Contains(FilterText.ToLower());
                case (AccountFilterOptions.Username):
                    return string.IsNullOrEmpty(FilterText) ? true :
                        account.Username.ToLower().Contains(FilterText.ToLower());
                default: return false;
            }
        }

        #endregion
    }
}
