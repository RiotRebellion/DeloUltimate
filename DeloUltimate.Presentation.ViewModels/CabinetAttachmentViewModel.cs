using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using DeloUltimate.Presentation.ViewModels.Models;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class CabinetAttachmentViewModel : ViewModel
    {
        private IDataImportService<Cabinet> _dataImportService;

        public CabinetAttachmentViewModel(IDataImportService<Cabinet> dataImportService)
        {
            _dataImportService = dataImportService;

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

        #region CabinetCollection

        private ObservableCollection<Cabinet> _cabinetCollection = new ObservableCollection<Cabinet>();

        private ObservableCollection<Cabinet> CabinetCollection
        {
            get => _cabinetCollection;
            set => Set(ref _cabinetCollection, value);
        }

        #endregion

        #region CabinetCollectionView

        private ListCollectionView _cabinetCollectionView;

        public ListCollectionView CabinetCollectionView
        {
            get => _cabinetCollectionView;
            set => Set(ref _cabinetCollectionView, value);
        }

        #endregion

        #region CabinetFilterOptions

        private CabinetFilterOptions _cabinetFilterOptions = CabinetFilterOptions.EmployeeName;

        public CabinetFilterOptions CabinetFilterOptions
        {
            get => _cabinetFilterOptions;
            set => Set(ref _cabinetFilterOptions, value);
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
                CabinetCollectionView.Filter += CabinetFilter;
            }
        }

        #endregion

        #endregion

        #region Commands

        #region FetchData
        public ICommand FetchData { get; private set; }

        private bool CanFetchDataExecute(object p) => IsDataFetched == false;

        private void OnFetchDataExecuted(object p)
        {
            CabinetCollection = new ObservableCollection<Cabinet>(_dataImportService.ImportFromDatabase());
            CabinetCollectionView = new ListCollectionView(CabinetCollection);
            IsDataFetched = true;
        }

        #endregion

        #endregion

        #region EventHandlers

        private bool CabinetFilter(object obj)
        {
            Cabinet cabinet = obj as Cabinet;
            switch (CabinetFilterOptions)
            {
                case (CabinetFilterOptions.EmployeeName):
                    return string.IsNullOrEmpty(FilterText) ? true :
                        cabinet.EmployeeName.ToLower().Contains(FilterText.ToLower());
                case (CabinetFilterOptions.DepartmentName):
                    return string.IsNullOrEmpty(FilterText) ? true :
                        cabinet.CabinetName.ToLower().Contains(FilterText.ToLower());
                default: return false;
            }
        }

        #endregion
    }
}
