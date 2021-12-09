using DataLibrary;
using DataLibrary.Models;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(WaypointId), nameof(WaypointId))]
    class QuizzesViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;
        public ObservableCollection<Quiz> Quizzes { get; }

        private List<Waypoint> _waypointList = new List<Waypoint>();
        public Command LoadQuizzesCommand { get; }
        public Command LoadUserRoutesCommand { get; }
        public Command AddQuizCommand { get; }

        private Quiz _selectedQuiz;

        private bool _userRoutesOnly = false;
        public string SearchText { get; set; }

        public long WaypointId { get; set; }

        public QuizzesViewModel()
        {
            Title = "All quizzes";

            Quizzes = new ObservableCollection<Quiz>();

            LoadQuizzesCommand = new Command(async () => await LoadQuizCommand());

            AddQuizCommand = new Command(AddQuiz);

            _encounterProcessor = EncounterProcessor.Instanse;

            _session = Session.Instanse;
        }
      
        async Task LoadQuizCommand()
        {
            if (!_userRoutesOnly)
            {
                IsBusy = true;

                try
                {
                    Quizzes.Clear();
                    var quizzes = await _encounterProcessor.GetMultipleWaypointQuestions(WaypointId);

                    foreach (var quiz in quizzes)
                    {
                        Quizzes.Add(quiz);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
            IsBusy = false;
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }

        public Quiz SelectedQuiz
        {
            get => _selectedQuiz;
            set
            {
                _selectedQuiz = value;
                OnQuizSelected(value);
            }
        }
        private async void OnQuizSelected(Quiz quiz)
        {

            await Shell.Current.GoToAsync($"{nameof(QuizAnswerPage)}?{nameof(QuizzesAnswersViewModel.QuizId)}={quiz.Id}");
        }

        private async void AddQuiz(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewQuizQuestionPage)}?{ nameof(NewQuizQuestionViewModel.Id)}={WaypointId}");
        }
    }
}
