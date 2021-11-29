using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data_Transfer_Objects
{
    public class QuizAnswerDTO
    {
        public long Id { get; set; }
        public long QuizId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
