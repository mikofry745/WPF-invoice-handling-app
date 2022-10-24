using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;
using System;

namespace ObsługaFaktur.Services.Navigation
{
    public class LayoutNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<NavigationBarViewModel> _CreateNavigationBarViewModel;
        private readonly Func<TViewModel> _CreateViewModel;

        public LayoutNavigationService(NavigationStore navigationStore, 
            Func<NavigationBarViewModel> createNavigationBarViewModel, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _CreateNavigationBarViewModel = createNavigationBarViewModel;
            _CreateViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new LayoutViewModel(_CreateNavigationBarViewModel(), _CreateViewModel());
        }
    }
}
