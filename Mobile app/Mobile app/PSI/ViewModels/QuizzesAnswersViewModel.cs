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
    [QueryProperty(nameof(QuizId), nameof(QuizId))]
    class QuizzesAnswersViewModel : BaseViewModel

    {
            private EncounterProcessor _encounterProcessor;
            private Session _session;
            public ObservableCollection<QuizAnswer> Answer { get; }
            public Command LoadQuizzesAnswersCommand { get; }
            public Command AddAnswerCommand { get; }
            public Command DeleteQuizCommand { get; }

            public Command EditQuizCommand { get; }

        private QuizAnswer _selectedAnswer;

            private bool _userRoutesOnly = false;
            public string SearchText { get; set; }

            public long QuizId { get; set; }

            public QuizzesAnswersViewModel()
            {
                Title = "Answers";

                Answer = new ObservableCollection<QuizAnswer>();

                LoadQuizzesAnswersCommand = new Command(async () => await LoadQuizAnswerCommand());

                AddAnswerCommand = new Command(AddAnswer);
            
            DeleteQuizCommand = new Command(DeleteQuizzes);

            EditQuizCommand = new Command(EditQuizzes);

            _encounterProcessor = EncounterProcessor.Instanse;

                _session = Session.Instanse;
            }

            async Task LoadQuizAnswerCommand()
            {
                if (!_userRoutesOnly)
                {
                    IsBusy = true;

                    try
                    {
                        Answer.Clear();
                        var answers = await _encounterProcessor.GetQuizById(QuizId);

                        foreach (var answer in answers.Answers)
                        {
                            Answer.Add(answer);
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

            public QuizAnswer SelectedAnswer
            {
                get => _selectedAnswer;
                set
                {
                    _selectedAnswer = value;
                    OnAnswerSelected(value);
                }
            }
            private async void OnAnswerSelected(QuizAnswer answer)
            {

                await Shell.Current.GoToAsync($"{nameof(EditAnswerPopup)}?{nameof(EditQuizAnswerViewModel.AnswerId)}={answer.Id}");
            }
        private async void AddAnswer(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewQuizAnswerPage)}?{ nameof(NewQuizQuestionViewModel.Id)}={QuizId}");
        }

        private async void DeleteQuizzes(object obj)
        {
            await _encounterProcessor.DeleteQuiz(QuizId);
            await Shell.Current.GoToAsync("..");
        }

        private async void EditQuizzes(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(EditQuizPopup)}?{ nameof(EditQuizViewModel.QuizId)}={QuizId}");
        }
    }
}
