using DataLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace PSI.ViewModels
{
    [QueryProperty(nameof(AnswerId), nameof(AnswerId))]
    class EditQuizAnswerViewModel : BaseViewModel
    {
        private EncounterProcessor _encounterProcessor;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public Command DeleteCommand { get; }

        private string answer;
        private long answerId;
        private bool isTrue;
        private bool isFalse;
        public long AnswerId
        {
            get
            {
                return answerId;
            }
            set
            {
                answerId = value;
                LoadAnswer(value);
            }
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

        public EditQuizAnswerViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _encounterProcessor = EncounterProcessor.Instanse;
            LoadAnswer(AnswerId);
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(answer);
        }
        public async void LoadAnswer(long idas)
        {
            try
            {
                var answer = await _encounterProcessor.GetQuizAnswer(idas);
                Answer = answer.Text;
                if (answer.IsCorrect)
                {
                    IsTrue = answer.IsCorrect;
                }
                else
                {
                    IsFalse = true;
                }

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
                if ((IsTrue && !IsFalse) || (!IsTrue && IsFalse))
                {
                    var answer = await _encounterProcessor.GetQuizAnswer(AnswerId);
                    answer.Text = Answer;
                    answer.IsCorrect = isTrue;
                    await _encounterProcessor.UpdateQuizAnswer(answer);
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Please select one:", "True or False!", "OK");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        private async void OnDelete(object obj)
        {
            await _encounterProcessor.DeleteQuizAnswer(AnswerId);
            await Shell.Current.GoToAsync("..");
        }
    }
}
