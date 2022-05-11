using DeloUltimate.Domain.Entities;
using DeloUltimate.Presentation.ViewModels.Abstract;
using DeloUltimate.Services.Interfaces;
using System.Collections.ObjectModel;

namespace DeloUltimate.Presentation.ViewModels
{
    public class InvalidPersonViewModel : ViewModel
    {
        public InvalidPersonViewModel(IDataImportService<InvalidPerson> service)
        {
            InvalidPersonCollection = new ObservableCollection<InvalidPerson>(service.ImportFromDatabase());
        }

        #region Properties

        #region InvalidPersonCollection

        private ObservableCollection<InvalidPerson> _invalidPersonCollection;

        public ObservableCollection<InvalidPerson> InvalidPersonCollection
        {
            get => _invalidPersonCollection;
            set => Set(ref _invalidPersonCollection, value);
        }

        #endregion

        #endregion
    }
}
