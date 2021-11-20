namespace EncounterAPI.Models
{
    public class UserAnswer
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ChoiceId { get; set; }
        public string AnswerLetter { get; set; }
    }
}
