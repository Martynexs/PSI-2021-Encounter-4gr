namespace Entities.Data_Transfer_Objects
{
    public class UserAnswerDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long QuestionId { get; set; }
        public long ActualChoiceId { get; set; }
    }
}
