namespace EncounterAPI.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public long QuestionScore { get; set; }
        public long QuestionTimer { get; set; }
        public long QuizId { get; set; }
    }
}
