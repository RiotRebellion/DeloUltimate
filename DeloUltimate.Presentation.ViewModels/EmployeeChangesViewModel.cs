using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using DeloUltimate.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class EmployeeChangesViewModel : ViewModel
    {
        #region Fields

        private readonly IDataImportService<Employee> _importService;
        private readonly IDataExportService<Employee> _exportService;

        private IEnumerable<Employee> _nextEmployeeCollection;
        private IEnumerable<Employee> _prevEmployeeCollection;

        #endregion

        public EmployeeChangesViewModel(IDataImportService<Employee> importService,
                                        IDataExportService<Employee> exportService)
        {
            _importService = importService;
            _exportService = exportService;

            #region Registration

            #region Commands

            FetchEmployees = new RelayCommand(OnFetchEmployeesExecuted, CanFetchEmployeesExecute);
            UploadOldDataFile = new RelayCommand(OnUploadOldDataFileExecuted, CanUploadOldDataFileExecute);
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

        #region IsDisplayingAll

        private bool _isDisplayingAll;

        public bool IsDisplayingAll
        {
            get => _isDisplayingAll;
            set => Set(ref _isDisplayingAll, value);
        }

        #endregion

        #region IsDisplayingNew

        private bool _isDisplayingNew;

        public bool IsDisplayingNew
        {
            get => _isDisplayingNew;
            set => Set(ref _isDisplayingNew, value);
        }

        #endregion

        #region IsDisplayingAll

        private bool _isDisplayingOld;

        public bool IsDisplayingOld
        {
            get => _isDisplayingOld;
            set => Set(ref _isDisplayingOld, value);
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

        private string _nextDateTime;

        public string NextDateTime
        {
            get => _nextDateTime;
            set => Set(ref _nextDateTime, value);
        }

        #endregion

        #region PrevDataTime

        private string _prevDateTime;

        public string PrevDateTime
        {
            get => _prevDateTime;
            set => Set(ref _prevDateTime, value);
        }

        #endregion

        #endregion

        #region Commands

        #region FetchEmployees

        public ICommand FetchEmployees { get; private set; }

        private bool CanFetchEmployeesExecute(object p) => true;

        private void OnFetchEmployeesExecuted(object p)
        {
            PrevDateTime = DateTime.Now.ToString("yyyy-MM-dd");
            _prevEmployeeCollection = _importService.ImportFromDatabase();
            _exportService.ExportToXML(_prevEmployeeCollection);
            EmployeeCollection = new ObservableCollection<Employee>(_prevEmployeeCollection);
        }

        #endregion

        #region UploadOldDataFile

        public ICommand UploadOldDataFile { get; private set; }

        private bool CanUploadOldDataFileExecute(object p) => true;

        private void OnUploadOldDataFileExecuted(object p)
        {
            _prevEmployeeCollection = _importService.ImportFromXML(ref _prevDateTime);
            if (_prevEmployeeCollection == null) return;
            

        }
        #endregion

        #endregion
    }
}
