using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class EmployeeChangesViewModel : ViewModel
    {
        #region Fields

        private readonly IDataImportService<Employee> _importService;
        private readonly IDataExportService<Employee> _exportService;
        private readonly ICompareService<Employee> _compareService;

        private IEnumerable<Employee> _nextEmployeeCollection = new List<Employee>();
        private IEnumerable<Employee> _prevEmployeeCollection = new List<Employee>();

        #endregion

        public EmployeeChangesViewModel(IDataImportService<Employee> importService,
                                        IDataExportService<Employee> exportService,
                                        ICompareService<Employee> compareService)
        {
            _importService = importService;
            _exportService = exportService;
            _compareService = compareService;

            #region Registration

            #region Commands

            FetchEmployees = new RelayCommand(OnFetchEmployeesExecuted, CanFetchEmployeesExecute);
            UploadPrevDataFile = new RelayCommand(OnUploadPrevDataFileExecuted, CanUploadPrevDataFileExecute);
            UploadNextDataFile = new RelayCommand(OnUploadNextDataFileExecuted, CanUploadNextDataFileExecute);
            #endregion

            #endregion
        }

        #region Properties

        #region DataGridHeaders

        private Collection<string> _dataGridHeaders;
        
        public Collection<string> _dataGrid
        {
            get => _dataGridHeaders;
            set => Set(ref _dataGridHeaders, value);
        }

        #endregion

        #region IsDataUploaded

        private bool _isDataUploaded = false;

        public bool IsDataUploaded
        {
            get => _isDataUploaded;
            set => Set(ref _isDataUploaded, value);
        }

        #endregion

        #region EmployeeCollection

        private ObservableCollection<Employee> _employeeCollection;

        public ObservableCollection<Employee> EmployeeCollection
        {
            get => _employeeCollection;
            set => Set(ref _employeeCollection, value);
        }

        #endregion

        #region NextDataTime

        private string _nextDataTime;

        public string NextDataTime
        {
            get => _nextDataTime;
            set => Set(ref _nextDataTime, value);
        }

        #endregion

        #region PrevDataTime

        private string _prevDataTime;

        public string PrevDataTime
        {
            get => _prevDataTime;
            set => Set(ref _prevDataTime, value);
        }

        #endregion

        #endregion

        #region Commands

        #region FetchEmployees

        public ICommand FetchEmployees { get; private set; }

        private bool CanFetchEmployeesExecute(object p) => true;

        private void OnFetchEmployeesExecuted(object p)
        {
            _prevEmployeeCollection = _importService?.ImportFromDatabase();
            if(_prevEmployeeCollection.Any()) 
            {
                NextDataTime = DateTime.Now.ToString("yyyy.MM.dd");
                IsDataUploaded = true;
                _exportService.ExportToXML(_prevEmployeeCollection);
                EmployeeCollection = new ObservableCollection<Employee>(_prevEmployeeCollection);
            }
        }

        #endregion

        #region UploadPrevDataFile

        public ICommand UploadPrevDataFile { get; private set; }

        private bool CanUploadPrevDataFileExecute(object p) => true;

        private void OnUploadPrevDataFileExecuted(object p)
        {
            string tempTime = "";
            _prevEmployeeCollection = _importService?.ImportFromXML(ref tempTime);
            if (_prevEmployeeCollection.Any())
            {
                IsDataUploaded = true;
                PrevDataTime = tempTime;
                ExecuteComparing();
            }
        }

        #endregion

        #region UploadNextDataFile

        public ICommand UploadNextDataFile { get; private set; }

        private bool CanUploadNextDataFileExecute(object p) => IsDataUploaded;

        private void OnUploadNextDataFileExecuted(object p)
        {
            string tempTime = "";
            _nextEmployeeCollection = _importService?.ImportFromXML(ref tempTime);
            if (_nextEmployeeCollection.Any()) 
            { 
                NextDataTime = tempTime;
                ExecuteComparing();
            }
        }
        #endregion

        #endregion

        #region Methods

        #region ExecuteComparing

        public void ExecuteComparing()
        {
            if (_prevEmployeeCollection.Any() && _nextEmployeeCollection.Any())
            {
                var tempCollection = _compareService.GetCollectionsDifference(_prevEmployeeCollection, _nextEmployeeCollection);
                EmployeeCollection = new ObservableCollection<Employee>(tempCollection);
            }
        }

        #endregion
        
        #endregion
    }
}
