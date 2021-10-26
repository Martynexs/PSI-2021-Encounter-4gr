using Encounter.ViewModels;
using System;

namespace Encounter.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;
        public ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
