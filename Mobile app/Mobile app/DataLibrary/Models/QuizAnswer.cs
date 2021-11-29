using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class QuizAnswer
    {
        public long Id { get; set; }
        public long QuizId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
