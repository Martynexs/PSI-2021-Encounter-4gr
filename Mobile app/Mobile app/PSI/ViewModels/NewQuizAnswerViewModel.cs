using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    class NewQuizAnswerViewModel : BaseViewModel
    {
            private EncounterProcessor _encounterProcessor;
            private Session _session;

            private string answer;
        private bool isTrue;
        private bool isFalse;

            public long Id { get; set; }

            public NewQuizAnswerViewModel()
            {
                SaveCommand = new Command(OnSave, ValidateSave);
                CancelCommand = new Command(OnCancel);
                this.PropertyChanged +=
                    (_, __) => SaveCommand.ChangeCanExecute();
                _encounterProcessor = EncounterProcessor.Instanse;
                _session = Session.Instanse;
            }

            private bool ValidateSave()
            {
                return !String.IsNullOrWhiteSpace(answer);
            }

            public string Answer
            {
                get => answer;
                set => SetProperty(ref answer, value);
            }

             public bool IsTrue
             {
              get => isTrue;
              set => SetProperty(ref isTrue, value);
             }

        public bool IsFalse
        {
            get => isFalse;
            set => SetProperty(ref isFalse, value);
        }

        public Command SaveCommand { get; }
            public Command CancelCommand { get; }

            private async void OnCancel()
            {
                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }

            private async void OnSave()
            {
            if ((IsTrue && !IsFalse) || (!IsTrue && IsFalse))
            {
                QuizAnswer newQuizAnswer = new QuizAnswer()
                {
                    Text = Answer,
                    QuizId = Id,
                    IsCorrect = IsTrue
                };

                await _encounterProcessor.CreateQuizAnswer(newQuizAnswer);

                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Please select one:", "True or False!", "OK");
                }
            }
        }
}
