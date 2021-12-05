using DataLibrary.Models;
using Map3.ViewModels;
using PSI.Models;
using PSI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        private readonly List<Quiz> _quizQuestions;
        private int _currentIdx;
        private string _currentQuestionText;
        public ObservableCollection<VisualAnswer> SelectableAnswers { get; }

        public Command SubmitAnswerCommand { get; }

        public Command SkipQuestionCommand { get; }

        public QuizViewModel()
        {
            _quizQuestions = new List<Quiz>();
            foreach (Quiz qq in WalkingSession.GetQuizQuestions())
            {
                _quizQuestions.Add(qq);
            }
            SelectableAnswers = new ObservableCollection<VisualAnswer>();

            LoadQuestion(0);
            SubmitAnswerCommand = new Command(async () => await ConfirmQuizStep());
            SkipQuestionCommand = new Command(async () => await SkipQuestion());
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public int CurrentIdx
        {
            get => _currentIdx;
            set => SetProperty(ref _currentIdx, value);
        }

        public int CurrentQuestionDisplayableIndex
        {
            get => CurrentIdx + 1;
        }


        public int QuestionsCount
        {
            get => _quizQuestions.Count;
        }

        public string CurrentQuestionText
        {
            get => _currentQuestionText;
            set => SetProperty(ref _currentQuestionText, value);
        }

        public async Task SkipQuestion()
        {
            await GoToNextQuizStep();
        }

        public async Task ConfirmQuizStep()
        {
            VisualAnswer selected = GetSingleSelectedAnswer();
            if (selected == null)
            {
                await DisplayAlert("Oops", "Please choose the answer", "ok");
                return;
            }

            if (selected.IsCorrect)
            {
                await DisplayAlert("Yahoo!", "You got this one correct!", "ok");
            } else
            {
                await DisplayAlert("Error!", "No no no, it's wrong answer, better luck next time!", "ok");
            }

            await GoToNextQuizStep();
        }

        private async Task GoToNextQuizStep()
        {
            if (HasMoreQuestions())
            {
                LoadQuestion(CurrentIdx + 1);
                return;
            }
            
            // Quiz finished:
            CurrentIdx = 0;
            SelectableAnswers.Clear();
            CurrentQuestionText = "";
            WalkingSession.AssignQuiz(null);
            await DisplayAlert("Quiz completed", "Great, you finished the quiz, let's go back to map.", "ok");

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"{nameof(Map)}?{nameof(MapViewModel.ReloadFromWalkingSession)}={true}");
        }

        private void LoadQuestion(int index)
        {
            CurrentIdx = index;
            SelectableAnswers.Clear();
            CurrentQuestionText = _quizQuestions[index].Question;
            foreach (QuizAnswer ans in _quizQuestions[index].Answers)
            {
                SelectableAnswers.Add(new VisualAnswer()
                {
                    Id = ans.Id,
                    QuizId = ans.QuizId,
                    IsCorrect = ans.IsCorrect,
                    Text = ans.Text,
                    IsMarked = false
                });
            }
        }

        private List<VisualAnswer> GetSelectedAnswers()
        {
            return SelectableAnswers.Where(x => x.IsMarked).ToList();
        }

        private VisualAnswer GetSingleSelectedAnswer()
        {
            List<VisualAnswer> chosenAnswers = GetSelectedAnswers();
            if (chosenAnswers == null || chosenAnswers.Count == 0)
            {
                return null;
            }

            if (chosenAnswers.Count == 1)
            {
                return chosenAnswers[0];
            }
            throw new InvalidOperationException("Single answer expected, but found many, exactly: " + chosenAnswers.Count);
        }

        private bool HasMoreQuestions()
        {
            return (CurrentIdx + 1) < _quizQuestions.Count;
        }
    }
}
