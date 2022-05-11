using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Presentation.ViewModels.Commands;
using System.Windows.Input;

namespace DeloUltimate.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly AccountActivityViewModel _accountActivitiesViewModel;
        private readonly AccountAttachmentViewModel _accountAttachmentsViewModel;
        private readonly CabinetAttachmentViewModel _cabinetAttachmentsViewModel;
        private readonly InvalidPersonViewModel _deletedPersonViewModel;
        private readonly EmployeeChangesViewModel _employeeChangesViewModel;

        public MainWindowViewModel(
                                   AccountAttachmentViewModel accountAttachmentsViewModel,
                                   CabinetAttachmentViewModel cabinetAttachmentsViewModel,
                                   InvalidPersonViewModel deletedPersonViewModel,
                                   EmployeeChangesViewModel employeeChangesViewModel, AccountActivityViewModel accountActivitiesViewModel)
        {
            _accountAttachmentsViewModel = accountAttachmentsViewModel;
            _cabinetAttachmentsViewModel = cabinetAttachmentsViewModel;
            _deletedPersonViewModel = deletedPersonViewModel;
            _employeeChangesViewModel = employeeChangesViewModel;
            _accountActivitiesViewModel = accountActivitiesViewModel;

            #region Fiels/Properties

            CurrentViewModel = new HomeViewModel();

            #endregion

            #region Commands

            ShowAccountActivitiesView = new RelayCommand(OnShowAccountActivitiesViewExecute);
            ShowAccountAttachmentsView = new RelayCommand(OnShowAccountAttachmentsViewExecute);
            ShowCabinetAttachmentsView = new RelayCommand(OnShowCabinetAttachmentsViewExecute);
            ShowDeletedPersonView = new RelayCommand(OnShowDeletedPersonViewExecute);
            ShowEmployeeChangesView = new RelayCommand(OnShowEmployeeChangesViewExecute);

            #endregion
        }

        #region Properties

        #region CurrentViewModel

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        #endregion

        #endregion

        #region Commands

        #region ShowAccountActivitiesView

        public ICommand ShowAccountActivitiesView { get; private set; }

        private void OnShowAccountActivitiesViewExecute(object p)
        {
            CurrentViewModel = _accountActivitiesViewModel;
        }

        #endregion

        #region ShowAccountAttachmentsView

        public ICommand ShowAccountAttachmentsView { get; private set; }

        private void OnShowAccountAttachmentsViewExecute(object p)
        {
            CurrentViewModel = _accountAttachmentsViewModel;
        }

        #endregion

        #region ShowCabinetAttachmentsView

        public ICommand ShowCabinetAttachmentsView { get; private set; }

        private void OnShowCabinetAttachmentsViewExecute(object p)
        {
            CurrentViewModel = _cabinetAttachmentsViewModel;
        }

        #endregion

        #region ShowDeletedPersonView

        public ICommand ShowDeletedPersonView { get; private set; }

        private void OnShowDeletedPersonViewExecute(object p)
        {
            CurrentViewModel = _deletedPersonViewModel;
        }

        #endregion

        #region ShowEmployeeChangesView

        public ICommand ShowEmployeeChangesView { get; private set; }

        private void OnShowEmployeeChangesViewExecute(object p)
        {
            CurrentViewModel = _employeeChangesViewModel;
        }

        #endregion

        #endregion

    }
}
