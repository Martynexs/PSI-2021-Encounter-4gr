using DataLibrary.Models;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace PSI.Models
{
    public class WalkingSession
    {
        private static WalkingSession Current;
        
        // Walking state:
        private bool _walkingActive;
        private long _routeId;
        private VisualWaypoint _currentGoalWaypoint;
        private readonly List<VisualWaypoint> _allWaypoints;
        private readonly List<VisualWaypoint> _goalWaypointsLeft;
        private Location _lastKnownLocation;

        // Quiz state:
        private bool _quizActive;
        private bool _quizJustFinished;
        private List<Quiz> _quizQuestions;
        private int _quizCurrentIndex;
        private bool _quizCurrentQuestionAnswered;
        private int _quizEarnedPoints;
        private List<VisualAnswer> _quizSelectableAnswers;

        private WalkingSession(List<VisualWaypoint> waypoints, long routeId)
        {
            _allWaypoints = new List<VisualWaypoint>(waypoints); // uneditable copy
            _goalWaypointsLeft = waypoints;
            _routeId = routeId;
            _walkingActive = false;
            _quizActive = false;
        }

        public static void InitializeWaypointsBeforeWalking(List<VisualWaypoint> goalWaypointsLeft, long routeId)
        {
            Current = new WalkingSession(goalWaypointsLeft, routeId);
        }

        public static void Start()
        {
            if (Current == null)
            {
                throw new InvalidOperationException("Can not activate walking session, because it was not initialized!");
            }
            Current._walkingActive = true;
        }

        public static void FinishAndClear()
        {
            Current = null;
        }

        public static bool IsWalkingStarted()
        {
            return Current != null && Current._walkingActive;
        }

        public static bool IsQuizStarted()
        {
            return IsWalkingStarted() && Current._quizActive;
        }

        private static void RequireWalkingStarted()
        {
            if (!IsWalkingStarted())
            {
                throw new InvalidOperationException("Walking session must be active at this point!");
            }
        }

        private static void RequireQuizStarted()
        {
            RequireWalkingStarted();
            if (!IsQuizStarted())
            {
                throw new InvalidOperationException("Quiz needs to be started at this point!");
            }
        }

        public static List<VisualWaypoint> GetAllWaypoints()
        {
            RequireWalkingStarted();
            return Current._allWaypoints;
        }

        public static VisualWaypoint GetCurrentGoalWaypoint()
        {
            RequireWalkingStarted();
            return Current._currentGoalWaypoint;
        }

        public static bool HasGoalWaypointsLeft()
        {
            return Current._goalWaypointsLeft != null && Current._goalWaypointsLeft.Count > 0;
        }

        public static bool IsGoalWaypointReached(Location deviceLocation)
        {
            RequireWalkingStarted();
            if (deviceLocation == null)
            {
                return false;
            }

            VisualWaypoint currentGoalWaypoint = GetCurrentGoalWaypoint();
            if (currentGoalWaypoint == null)
            {
                return true;
            }
            return (Math.Abs(deviceLocation.Latitude - currentGoalWaypoint.Lat) < 0.001) && (Math.Abs(deviceLocation.Longitude - currentGoalWaypoint.Long) < 0.001);
        }

        public static VisualWaypoint SetPickedWaypoint(VisualWaypoint selectedWaypoint)
        {
            RequireWalkingStarted();

            if (!HasGoalWaypointsLeft())
            {
                return null;
            }

            int selectedIndex = Current._goalWaypointsLeft.IndexOf(selectedWaypoint);
            if (selectedIndex == -1)
            {
                throw new InvalidOperationException("Oops, selected waypoint does not belong to this route or is already visited.");
            }

            Current._currentGoalWaypoint = selectedWaypoint;
            return selectedWaypoint;
        }

        public static void MarkCurrentGoalWaypointReached()
        {
            Current._goalWaypointsLeft.Remove(Current._currentGoalWaypoint);
        }

        public static VisualWaypoint MoveToNextWaypoint(VisualWaypoint selectedWaypoint)
        {
            RequireWalkingStarted();
            return SetPickedWaypoint(selectedWaypoint);
        }

        public static bool IsTheLastGoalWaypoint()
        {
            RequireWalkingStarted();
            return Current._goalWaypointsLeft != null && Current._goalWaypointsLeft.Count == 1;
        }

        public static bool CheckMoved(Location currentLocation)
        {
            RequireWalkingStarted();
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
            return Current._routeId;
        }

        public static void StartQuiz(List<Quiz> questions)
        {
            RequireWalkingStarted();
            if (Current._quizActive)
            {
                throw new InvalidOperationException("Quiz already started");
            }
            Current._quizQuestions = questions;
            Current._quizCurrentIndex = 0;
            Current._quizEarnedPoints = 0;
            Current._quizCurrentQuestionAnswered = false;
            Current._quizActive = true;
        }

        public static bool HasQuiz()
        {
            RequireWalkingStarted();
            return Current._quizQuestions != null;
        }

        public static void FinishQuiz()
        {
            RequireWalkingStarted();
            Current._quizActive = false;
            Current._quizQuestions = null;
            Current._quizJustFinished = true;
        }

        public static bool HasQuizJustFinished()
        {
            RequireWalkingStarted();
            return Current._quizJustFinished;
        }

        public static void ClearQuizJustFinishedMarker()
        {
            RequireWalkingStarted();
            Current._quizJustFinished = false;
        }

        public static List<Quiz> GetQuizQuestions()
        {
            RequireQuizStarted();
            return Current._quizQuestions;
        }

        public static int GetQuizCurrentIndex()
        {
            RequireQuizStarted();
            return Current._quizCurrentIndex;
        }

        public static void SetQuizCurrentIndex(int index)
        {
            RequireQuizStarted();
            Current._quizCurrentIndex = index;
        }

        public static int GetQuizEarnedPoints()
        {
            RequireQuizStarted();
            return Current._quizEarnedPoints;
        }

        public static void SetQuizEarnedPoints(int points)
        {
            RequireQuizStarted();
            Current._quizEarnedPoints = points;
        }

        public static bool GetQuizCurrentQuestionAnswered()
        {
            RequireQuizStarted();
            return Current._quizCurrentQuestionAnswered;
        }

        public static void SetQuizCurrentQuestionAnswered(bool answered)
        {
            RequireQuizStarted();
            Current._quizCurrentQuestionAnswered = answered;
        }

        public static List<VisualAnswer> GetQuizSelectableAnswers()
        {
            RequireQuizStarted();
            return Current._quizSelectableAnswers;
        }

        public static void SetQuizSelectableAnswers(List<VisualAnswer> selectableAnswers)
        {
            RequireQuizStarted();
            Current._quizSelectableAnswers = selectableAnswers;
        }

    }
}
