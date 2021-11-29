using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class QuizAnswers
    {
        public long Id { get; set; }
        public long QuizId { get; set; }
        public virtual Quiz quiz { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
