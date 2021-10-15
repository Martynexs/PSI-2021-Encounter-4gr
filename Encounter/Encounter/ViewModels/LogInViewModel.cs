using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encounter.Commands;
using Encounter.Stores;
using System.Windows.Input;

namespace Encounter.ViewModels
{
    public class LogInViewModel : ViewModelBase
    {
        public ICommand LoadHomePage { get; }
      
        public LogInViewModel(NavigationStore navigationStore)
        {
            LoadHomePage = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
           
        }
    }
}
