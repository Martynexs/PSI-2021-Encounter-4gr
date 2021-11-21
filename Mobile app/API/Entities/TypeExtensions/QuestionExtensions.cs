using EncounterAPI.Models;
using Entities.Data_Transfer_Objects;

namespace Entities.TypeExtensions
{
    public static class QuestionExtension
    {
        public static QuestionDTO ToDTO(this Question qu)
        {
            return new QuestionDTO
            {
                Id = qu.Id,
                Position = qu.Position,
                Text = qu.Text,
                Type = qu.Type,
                SecondsToAnswer = qu.SecondsToAnswer,
                QuizId = qu.QuizId
            };
        }

        public static Question ToEFModel(this QuestionDTO questionDTO)
        {
            return new Question
            {
                Id = questionDTO.Id,
                Position = questionDTO.Position,
                Text = questionDTO.Text,
                Type = questionDTO.Type,
                SecondsToAnswer = questionDTO.SecondsToAnswer,
                QuizId = questionDTO.QuizId
            };
        }
    }
}
