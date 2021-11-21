using EncounterAPI.Models;
using Entities.Data_Transfer_Objects;

namespace Entities.TypeExtensions
{
    public static class UserAnswerExtensions
    {
        public static UserAnswerDTO ToDTO(this UserAnswer ua)
        {
            return new UserAnswerDTO
            {
                Id = ua.Id,
                UserId = ua.UserId,
                QuestionId = ua.QuestionId,
                ActualChoiceId = ua.ActualChoiceId
            };
        }

        public static UserAnswer ToEFModel(this UserAnswerDTO userAnswerDTO)
        {
            return new UserAnswer
            {
                Id = userAnswerDTO.Id,
                UserId = userAnswerDTO.UserId,
                QuestionId = userAnswerDTO.QuestionId,
                ActualChoiceId = userAnswerDTO.ActualChoiceId
            };
        }
    }
}
