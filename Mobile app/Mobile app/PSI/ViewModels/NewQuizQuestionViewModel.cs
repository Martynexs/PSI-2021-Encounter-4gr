using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    class NewQuizQuestionViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        private Session _session;

        private string quizQuestion;

        public long Id { get; set; }

        public NewQuizQuestionViewModel()
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
            return !String.IsNullOrWhiteSpace(quizQuestion);
        }

        public string QuizQuestion
        {
            get => quizQuestion;
            set => SetProperty(ref quizQuestion, value);
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
            Quiz newQuiz = new Quiz()
            {
                Question = QuizQuestion,
                WaypointId = Id
            };

            await _encounterProcessor.CreateQuiz(newQuiz);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
