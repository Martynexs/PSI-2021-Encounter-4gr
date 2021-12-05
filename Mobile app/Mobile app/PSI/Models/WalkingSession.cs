using DataLibrary.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace PSI.Models
{
    public class WalkingSession
    {
        private static WalkingSession Current;
        private readonly List<VisualWaypoint> GoalWaypointsLeft;
        private Location LastKnownLocation;
        private List<Quiz> QuizQuestions; // when quiz is active it's not null

        private WalkingSession(List<VisualWaypoint> waypoints)
        {
            GoalWaypointsLeft = waypoints;
        }

        public static VisualWaypoint CurrentGoalWaypoint()
        {
            if (!HasGoalWaypointsLeft())
            {
                return null;
            }
            return Current.GoalWaypointsLeft[0];
        }

        public static bool HasGoalWaypointsLeft()
        {
            return Current != null && Current.GoalWaypointsLeft != null && Current.GoalWaypointsLeft.Count > 0;
        }

        public static void ResetTo(List<VisualWaypoint> goalWaypointsLeft)
        {
            Current = new WalkingSession(goalWaypointsLeft);
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

        public static VisualWaypoint MoveToNextGoalWaypoint()
        {
            if (!HasGoalWaypointsLeft())
            {
                return null;
            }

            Current.GoalWaypointsLeft.RemoveAt(0);

            if (Current.GoalWaypointsLeft.Count == 0)
            {
                return null;
            }
            return Current.GoalWaypointsLeft[0];
        }

        public static bool IsTheLastGoalWaypoint()
        {
            return Current != null && Current.GoalWaypointsLeft != null && Current.GoalWaypointsLeft.Count == 1;
        }

        public static bool CheckMoved(Location currentLocation)
        {
            if (Current == null)
            {
                return false;
            }

            if (Current.LastKnownLocation == null)
            {
                Current.LastKnownLocation = currentLocation;
                return true;
            }

            if (currentLocation.Latitude == Current.LastKnownLocation.Latitude && currentLocation.Longitude == Current.LastKnownLocation.Longitude)
            {
                return false;
            }

            Current.LastKnownLocation = currentLocation;
            return true;
        }

        public static void AssignQuiz(List<Quiz> questions)
        {
            Current.QuizQuestions = questions;
        }

        public static bool HasQuiz()
        {
            return Current.QuizQuestions != null;
        }

        public static List<Quiz> GetQuizQuestions()
        {
            return Current.QuizQuestions;
        }
    }
}
