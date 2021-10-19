using Encounter.IO;
using Encounter.Models;
using Encounter.ViewModels;

namespace Encounter.Commands.AboutRoute
{
    public class VoteCommand : CommandBase
    {
        private AboutRouteViewModel _aboutRouteViewModel;
        private User _user;
        private Route _route;
        public VoteCommand(AboutRouteViewModel aboutRouteViewModel, User user, Route route)
        {
            _aboutRouteViewModel = aboutRouteViewModel;
            _user = user;
            _route = route;
        }
        public override void Execute(object parameter)
        {
            if (_aboutRouteViewModel.OldUserRating == 0)
            {
                _route.Raters += 1;
                _route.RatingSum += _aboutRouteViewModel.UserRating;
                _route.Rating = _route.RatingSum / (double)_route.Raters;
            }
            else
            {
                _route.RatingSum = _route.RatingSum + _aboutRouteViewModel.UserRating - _aboutRouteViewModel.OldUserRating;
                _route.Rating = _route.RatingSum / (double)_route.Raters;
            }
            DatabaseFunctions.SubmitRating(_aboutRouteViewModel.UserRating, _route, _user);
            _aboutRouteViewModel.ReloadRoute();
        }
    }
}
