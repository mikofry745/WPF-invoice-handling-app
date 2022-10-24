﻿using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;
using System;

namespace ObsługaFaktur.Services.Navigation
{
    public class ModalNavigationService<TViewModel> : INavigationService
        where TViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public ModalNavigationService(ModalNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
