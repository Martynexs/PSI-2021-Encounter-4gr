using Encounter.ViewModels;

namespace Encounter.Commands.AboutRoute
{
    public class CloseAboutCommand : CommandBase
    {
        private AboutRouteViewModel _aboutRouteViewModel;

        public CloseAboutCommand(AboutRouteViewModel aboutRouteViewModel)
        {
            _aboutRouteViewModel = aboutRouteViewModel;
        }

        public override void Execute(object parameter)
        {
            _aboutRouteViewModel.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
