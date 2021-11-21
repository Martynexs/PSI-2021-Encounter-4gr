using EncounterAPI.Models;
using Entities.Data_Transfer_Objects;
using Entities.Data_Transfer_Objects.TypeConverters;

namespace Entities.TypeExtensions
{
    public static class QuestionChoiceExtensions
    {
        public static QuestionChoiceDTO ToDTO(this QuestionChoice qc)
        {
            return new QuestionChoiceDTO
            {
                Id = qc.Id,
                Position = qc.Position,
                Letter = qc.Letter,
                Text = qc.Text,
                IsRight = BooleanIntegerConverter.Instance.IntegerToBool(qc.IsRight),
                SelectedScore = qc.SelectedScore,
                UnselectedScore = qc.SelectedScore,
                QuestionId = qc.QuestionId
            };
        }

        public static QuestionChoice ToEFModel(this QuestionChoiceDTO questionChoiceDTO)
        {
            return new QuestionChoice
            {
                Id = questionChoiceDTO.Id,
                Position = questionChoiceDTO.Position,
                Letter = questionChoiceDTO.Letter,
                Text = questionChoiceDTO.Text,
                IsRight = BooleanIntegerConverter.Instance.BoolToInteger(questionChoiceDTO.IsRight),
                SelectedScore = questionChoiceDTO.SelectedScore,
                UnselectedScore = questionChoiceDTO.SelectedScore,
                QuestionId = questionChoiceDTO.QuestionId
            };
        }
    }
}
