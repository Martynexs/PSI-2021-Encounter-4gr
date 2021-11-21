using EncounterAPI.Models;
using Entities.Data_Transfer_Objects;

namespace Entities.TypeExtensions
{
    public static class QuizExtension
    {
        public static QuizDTO ToDTO(this Quiz qz)
        {
            return new QuizDTO
            {
                Id = qz.Id,
                WaypointId = qz.WaypointId
            };
        }

        public static Quiz ToEFModel(this QuizDTO quizDTO)
        {
            return new Quiz
            {
                Id = quizDTO.Id,
                WaypointId = quizDTO.WaypointId
            };
        }
    }
}
