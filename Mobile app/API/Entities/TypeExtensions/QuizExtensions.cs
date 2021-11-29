using Entities.Data_Transfer_Objects;
using Entities.Models;

namespace Entities.TypeExtensions
{
    public static class QuizExtensions
    {
        public static QuizDTO ToDTO(this Quiz quiz)
        {
            return new QuizDTO
            {
                Id = quiz.Id,
                WaypointId = quiz.WaypointId,
                Question = quiz.Question
            };
        }

        public static Quiz ToEFModel (this QuizDTO quizDTO)
        {
            return new Quiz
            {
                Id = quizDTO.Id,
                WaypointId = quizDTO.WaypointId,
                Question = quizDTO.Question
            };
        }

        public static QuizAnswerDTO ToDTO(this QuizAnswers answer)
        {
            return new QuizAnswerDTO
            {
                Id = answer.Id,
                QuizId = answer.QuizId,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect
            };
        }

        public static QuizAnswers ToEFModel(this QuizAnswerDTO answerDTO)
        {
            return new QuizAnswers
            {
                Id = answerDTO.Id,
                QuizId = answerDTO.QuizId,
                Text = answerDTO.Text,
                IsCorrect = answerDTO.IsCorrect
            };
        }
    }
}
