namespace EncounterAPI.Models
{
    public class UserAnswer
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long QuestionId { get; set; }
        public long ActualChoiceId { get; set; }
    }
}
