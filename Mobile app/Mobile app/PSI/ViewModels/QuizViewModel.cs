using DataLibrary.Models;
using PSI.Models;
using System.Collections.Generic;

namespace PSI.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        private List<Quiz> _quizQuestions;
        private int _currentIdx;

        public QuizViewModel()
        {
            _quizQuestions = WalkingSession.GetQuizQuestions();
            _currentIdx = 0;
        }

        public int CurrentQuestionDisplayableIndex
        {
            get => _currentIdx + 1;
        }

        public int QuestionsCount
        {
            get => _quizQuestions.Count;
        }

        public string CurrentQuestionText
        {
            get => _quizQuestions[_currentIdx].Question;
        }
    }
}
