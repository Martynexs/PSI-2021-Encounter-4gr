using DataLibrary.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace PSI.Models
{
    public class WalkingSession
    {
        private static WalkingSession Current;
        private long RouteId;
        private VisualWaypoint _nextWaypoint;
        private readonly List<VisualWaypoint> _goalWaypointsLeft;
        private Location _lastKnownLocation;
        private List<Quiz> _quizQuestions; // when quiz is active it's not null

        private WalkingSession(List<VisualWaypoint> waypoints, long routeId)
        {
            _goalWaypointsLeft = waypoints;
            RouteId = routeId;
        }

        public static VisualWaypoint CurrentGoalWaypoint()
        {
            if (!HasGoalWaypointsLeft())
            {
                return null;
            }
            return Current._nextWaypoint;
        }

        public static bool HasGoalWaypointsLeft()
        {
            return Current != null && Current._goalWaypointsLeft != null && Current._goalWaypointsLeft.Count > 0;
        }

        public static void ResetTo(List<VisualWaypoint> goalWaypointsLeft, long routeId)
        {
            Current = new WalkingSession(goalWaypointsLeft, routeId);
        }

        public static void Finish()
        {
            Current = null;
        }

        public static bool IsGoalWaypointReached(Location deviceLocation)
        {
            if (deviceLocation == null)
            {
                return false;
            }

            VisualWaypoint currentGoalWaypoint = CurrentGoalWaypoint();
            if (currentGoalWaypoint == null)
            {
                return true;
            }
            return (Math.Abs(deviceLocation.Latitude - currentGoalWaypoint.Lat) < 0.001) && (Math.Abs(deviceLocation.Longitude - currentGoalWaypoint.Long) < 0.001);
        }

        public static VisualWaypoint ChooseFirstWaypoint(VisualWaypoint selectedWaypoint)
        {
            if (!HasGoalWaypointsLeft())
            {
                return null;
            }

            if (Current._goalWaypointsLeft.Count == 0)
            {
                return null;
            }
            Current._nextWaypoint = selectedWaypoint;
            return selectedWaypoint;
        }

        public static VisualWaypoint MoveToNextGoalWaypoint(VisualWaypoint selectedWaypoint)
        {
            if (!HasGoalWaypointsLeft())
            {
                return null;
            }

            Current._goalWaypointsLeft.Remove(selectedWaypoint);

            if (Current._goalWaypointsLeft.Count == 0)
            {
                return null;
            }
            Current._nextWaypoint = selectedWaypoint;
            return selectedWaypoint;
        }

        public static bool IsTheLastGoalWaypoint()
        {
            return Current != null && Current._goalWaypointsLeft != null && Current._goalWaypointsLeft.Count == 1;
        }

        public static bool CheckMoved(Location currentLocation)
        {
            if (Current == null)
            {
                return false;
            }

            if (Current._lastKnownLocation == null)
            {
                Current._lastKnownLocation = currentLocation;
                return true;
            }

            if (currentLocation.Latitude == Current._lastKnownLocation.Latitude && currentLocation.Longitude == Current._lastKnownLocation.Longitude)
            {
                return false;
            }

            Current._lastKnownLocation = currentLocation;
            return true;
        }

        public static List<VisualWaypoint> GetLeftWaypoints()
        {
            if (Current == null)
            {
                return null;
            }
            return Current._goalWaypointsLeft;
        }

        public static long GetCurrentRouteId()
        {
            if (Current == null)
            {
                return -1;
            }
            return Current.RouteId;
        }

        public static void AssignQuiz(List<Quiz> questions)
        {
            Current._quizQuestions = questions;
        }

        public static bool HasQuiz()
        {
            return Current._quizQuestions != null;
        }

        public static List<Quiz> GetQuizQuestions()
        {
            return Current._quizQuestions;
        }
    }
}
