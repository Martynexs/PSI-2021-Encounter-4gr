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
        private bool _continueShown;
        private bool _submitShown;
        private string _currentQuestionText;
        private int _currentQuestionDisplayableIndex;
        private ObservableCollection<VisualAnswer> _selectableAnswers;

        public Command SubmitAnswerCommand { get; }

        public Command ContinueCommand { get; }

        public Command SkipQuestionCommand { get; }

        public QuizViewModel()
        {
            _quizQuestions = WalkingSession.GetQuizQuestions();
            SelectableAnswers = new ObservableCollection<VisualAnswer>();
            if (WalkingSession.GetQuizCurrentQuestionAnswered())
            {
                StartQuestion(WalkingSession.GetQuizCurrentIndex());
                SelectableAnswers.Clear();
                foreach (VisualAnswer sa in WalkingSession.GetQuizSelectableAnswers())
                {
                    SelectableAnswers.Add(sa);
                }
                SubmitShown = false;
                ContinueShown = true;
            } 
            else
            {
                StartQuestion(WalkingSession.GetQuizCurrentIndex());
                SubmitShown = true;
                ContinueShown = false;
            }
            bool multipleShown = GetCorrectAnswers().Count() > 1;
            MultipleShown = multipleShown;
            SingleShown = !multipleShown;
            
            SubmitAnswerCommand = new Command(async () => await SubmitAnswer());
            SkipQuestionCommand = new Command(async () => await SkipQuestion());
            ContinueCommand = new Command(async () => await Continue());
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public int CurrentIndex
        {
            get => WalkingSession.GetQuizCurrentIndex();
            set {
                WalkingSession.SetQuizCurrentIndex(value);
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
            set
            {
                SetProperty(ref _selectableAnswers, value);
            }
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

        public bool ContinueShown
        {
            get => _continueShown;
            set => SetProperty(ref _continueShown, value);
        }

        public bool SubmitShown
        {
            get => _submitShown;
            set => SetProperty(ref _submitShown, value);
        }

        public async Task SkipQuestion()
        {
            SubmitShown = true;
            ContinueShown = false;
            await Continue();
        }

        public int EarnedPoints
        {
            get => WalkingSession.GetQuizEarnedPoints();
            set {
                WalkingSession.SetQuizEarnedPoints(value);
                OnPropertyChanged();
            }
        }

        public async Task SubmitAnswer()
        {
            WalkingSession.SetQuizCurrentQuestionAnswered(true);
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
                    await DisplayAlert("Oops!", "No no no, it's wrong answer, better luck next time!", "ok");
                }
                SubmitShown = false;
                ContinueShown = true;
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

                await DisplayAlert("Ok", "Answers submitted, check your results", "ok");
                SubmitShown = false;
                ContinueShown = true;
                ColorifyAnswersAndGivePoints();
            }      
        }

        private void ColorifyAnswersAndGivePoints()
        {
            List<VisualAnswer> selectedAnswers = GetSelectedAnswers();
            foreach (VisualAnswer answ in SelectableAnswers)
            {
                answ.AllowSelecting = false;
                answ.Color = answ.IsCorrect ? "green" : "red";

                bool chosen = selectedAnswers.Find(x => x.Id == answ.Id) != null;
                if (chosen)
                {
                    if (answ.IsCorrect)
                    {
                        answ.PtsEarned = " (+2pts)";
                        EarnedPoints += 2;
                    } else
                    {
                        answ.PtsEarned = " (minus 3pts)";
                        EarnedPoints += -3;
                    }
                }
                else
                {
                    answ.PtsEarned = " (0pts)";
                }
            }
            WalkingSession.SetQuizSelectableAnswers(new List<VisualAnswer>(SelectableAnswers));
        }

        private async Task Continue()
        {
            WalkingSession.SetQuizCurrentQuestionAnswered(false);
            if (HasMoreQuestions())
            {
                StartQuestion(CurrentIndex + 1);
                return;
            }
            
            // Quiz finished:
            CurrentIndex = 0;
            SelectableAnswers.Clear();
            CurrentQuestionText = "";
            await DisplayAlert("Quiz completed", "Great, you finished the quiz and earned " + EarnedPoints + "pts, let's go back to map.", "ok");

            WalkingSession.FinishQuiz();
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"{nameof(Map)}?{nameof(MapViewModel.SelectedRouteId)}={WalkingSession.GetCurrentRouteId()}");
        }

        private void StartQuestion(int index)
        {
            CurrentIndex = index;
            SelectableAnswers.Clear();
            CurrentQuestionText = _quizQuestions[index].Question;
            foreach (QuizAnswer ans in _quizQuestions[index].Answers)
            {
                SelectableAnswers.Add(new VisualAnswer()
                {
                    Id = ans.Id,
                    IsCorrect = ans.IsCorrect,
                    Text = ans.Text,
                    IsMarked = false,
                    AllowSelecting = true,
                    Color = "Black"
                });
            }

            bool multipleShown = GetCorrectAnswers().Count > 1;
            MultipleShown = multipleShown;
            SingleShown = !multipleShown;
            SubmitShown = true;
            ContinueShown = false;
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
            List<VisualAnswer> markedAnswers = GetSelectedAnswers();
            if (markedAnswers == null || markedAnswers.Count == 0)
            {
                return null;
            }

            if (markedAnswers.Count == 1)
            {
                return markedAnswers[0];
            }
            throw new InvalidOperationException("Single answer expected, but found many, exactly: " + markedAnswers.Count);
        }

        private bool HasMoreQuestions()
        {
            return (CurrentIndex + 1) < _quizQuestions.Count;
        }
    }
}
