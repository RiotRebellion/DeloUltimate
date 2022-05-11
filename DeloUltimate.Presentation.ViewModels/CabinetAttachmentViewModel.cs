using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;

namespace DeloUltimate.Presentation.ViewModels
{
    public class CabinetAttachmentViewModel : ViewModel
    {
        public CabinetAttachmentViewModel(IDataImportService<Cabinet> service)
        {
            _cabinetCollection = new ObservableCollection<Cabinet>(service.ImportFromDatabase());
        }

        #region Properties

        #region CabinetCollection

        private ObservableCollection<Cabinet> _cabinetCollection;

        public ObservableCollection<Cabinet> CabinetCollection
        {
            get => _cabinetCollection;
            set => Set(ref _cabinetCollection, value);
        }

        #endregion

        #endregion
    }
}
