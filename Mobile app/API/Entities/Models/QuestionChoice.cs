namespace EncounterAPI.Models
{
    public class QuestionChoice
    {
        public long Id { get; set; }
        public long ChoicePosition { get; set; }
        public string ChoiceLetter { get; set; }
        public string ChoiceText { get; set; }
        public int AnswerIsRight { get; set; }
        public double AnswerSelectedScore { get; set; }
        public double AnswerUnselectedScore { get; set; }
        public long QuestionId { get; set; }
    }
}
