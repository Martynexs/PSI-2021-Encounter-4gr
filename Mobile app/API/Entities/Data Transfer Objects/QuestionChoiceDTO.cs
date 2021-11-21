namespace Entities.Data_Transfer_Objects
{
    public class QuestionChoiceDTO
    {
        public long Id { get; set; }
        public long Position { get; set; }
        public string Letter { get; set; }
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public double SelectedScore { get; set; }
        public double UnselectedScore { get; set; }
        public long QuestionId { get; set; }
    }
}
