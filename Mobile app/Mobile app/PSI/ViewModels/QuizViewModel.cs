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
        private bool _multipleShown;
        private bool _singleShown;
        private bool _proceedShown;
        private bool _submitShown;
        private int _points;
        private int _currentIdx;
        private int _currentQuestionDisplayableIndex;
        private string _currentQuestionText;
        private ObservableCollection<VisualAnswer> _selectableAnswers;

        public Command SubmitAnswerCommand { get; }

        public Command ProceedCommand { get; }

        public Command SkipQuestionCommand { get; }

        public QuizViewModel()
        {
            _quizQuestions = new List<Quiz>();
            foreach (Quiz qq in WalkingSession.GetQuizQuestions())
            {
                _quizQuestions.Add(qq);
            }
            SelectableAnswers = new ObservableCollection<VisualAnswer>();
            Points = 0;

            LoadQuestion(0);
            SubmitAnswerCommand = new Command(async () => await SubmitAnswerStep());
            SkipQuestionCommand = new Command(async () => await SkipQuestion());
            ProceedCommand = new Command(async () => await ProceedAnswerStep());
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public int CurrentIdx
        {
            get => _currentIdx;
            set {
                SetProperty(ref _currentIdx, value);
                CurrentQuestionDisplayableIndex = value + 1;
            }
        }

        public int CurrentQuestionDisplayableIndex
        {
            get => _currentQuestionDisplayableIndex;
            set => SetProperty(ref _currentQuestionDisplayableIndex, value);
        }

        public ObservableCollection<VisualAnswer> SelectableAnswers {
            get => _selectableAnswers;
            set => SetProperty(ref _selectableAnswers, value);
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

        public bool MultipleShown
        {
            get => _multipleShown;
            set => SetProperty(ref _multipleShown, value);
        }

        public bool SingleShown
        {
            get => _singleShown;
            set => SetProperty(ref _singleShown, value);
        }

        public bool ProceedShown
        {
            get => _proceedShown;
            set => SetProperty(ref _proceedShown, value);
        }

        public bool SubmitShown
        {
            get => _submitShown;
            set => SetProperty(ref _submitShown, value);
        }

        public async Task SkipQuestion()
        {
            SubmitShown = true;
            ProceedShown = false;
            await GoToNextQuizStep();
        }

        public int Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }

        public async Task SubmitAnswerStep()
        {
            if (SingleShown)
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
                }
                else
                {
                    await DisplayAlert("Error!", "No no no, it's wrong answer, better luck next time!", "ok");
                }
                SubmitShown = false;
                ProceedShown = true;
                ColorifyAnswersAndGivePoints();
                return;
            }

            if (MultipleShown)
            {
                List<VisualAnswer> selected = GetSelectedAnswers();
                if (selected == null || selected.Count == 0)
                {
                    await DisplayAlert("Oops", "Please choose at least one answer", "ok");
                    return;
                }

                await DisplayAlert("Ok", "Answer submitted", "ok");
                SubmitShown = false;
                ProceedShown = true;
                ColorifyAnswersAndGivePoints();
            }            
        }

        public async Task ProceedAnswerStep()
        {
            SubmitShown = true;
            ProceedShown = false;
            await GoToNextQuizStep();
        }

        private void ColorifyAnswersAndGivePoints()
        {
            List<VisualAnswer> selectedAnswers = GetSelectedAnswers();
            foreach (VisualAnswer a in SelectableAnswers)
            {
                a.Color = a.IsCorrect ? "green" : "red";

                bool chosen = selectedAnswers.Find(x => x.Id == a.Id) != null;
                if (chosen)
                {
                    if (a.IsCorrect)
                    {
                        a.PtsEarned = " (+2pts)";
                        Points += 2;
                    } else
                    {
                        a.PtsEarned = " (minus 3pts)";
                        Points -= 3;
                    }
                }
                else
                {
                    a.PtsEarned = " (0pts)";
                }
            }
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
            await DisplayAlert("Quiz completed", "Great, you finished the quiz and earned " + Points + "pts, let's go back to map.", "ok");

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

            bool multipleShown = GetCorrectAnswers().Count > 1;
            MultipleShown = multipleShown;
            SingleShown = !multipleShown;
            SubmitShown = true;
            ProceedShown = false;
        }

        private List<VisualAnswer> GetSelectedAnswers()
        {
            return SelectableAnswers.Where(x => x.IsMarked).ToList();
        }

        private List<VisualAnswer> GetCorrectAnswers()
        {
            return SelectableAnswers.Where(x => x.IsCorrect).ToList();
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
