namespace EncounterAPI.Models
{
    public class QuestionChoice
    {
        public long Id { get; set; }
        public long Position { get; set; }
        public string Letter { get; set; }
        public string Text { get; set; }
        public int IsRight { get; set; }
        public double SelectedScore { get; set; }
        public double UnselectedScore { get; set; }
        public long QuestionId { get; set; }
    }
}
