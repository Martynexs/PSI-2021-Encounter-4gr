namespace Entities.Data_Transfer_Objects
{
    public class QuestionDTO
    {
        public long Id { get; set; }
        public long Position { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public long SecondsToAnswer { get; set; }
        public long QuizId { get; set; }
    }
}
