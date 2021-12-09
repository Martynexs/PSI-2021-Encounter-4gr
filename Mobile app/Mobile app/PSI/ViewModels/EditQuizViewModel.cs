using DataLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(QuizId), nameof(QuizId))]
    class EditQuizViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private string questions;
        private long quizId;
        public long QuizId
        {
            get
            {
                return quizId;
            }
            set
            {
                quizId = value;
                LoadQuiz(value);
            }
        }

        public string Questions
        {
            get => questions;
            set => SetProperty(ref questions, value);
        }

        public EditQuizViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instanse;
            LoadQuiz(QuizId);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(questions);
        }
        public async void LoadQuiz(long idas)
        {
            try
            {
                var quiz = await _encounterProcessor.GetQuizById(idas);
                Questions = quiz.Question;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Quiz");
            }
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                var quiz = await _encounterProcessor.GetQuizById(QuizId);
                quiz.Question = Questions;
                await _encounterProcessor.UpdateQuiz(quiz);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
