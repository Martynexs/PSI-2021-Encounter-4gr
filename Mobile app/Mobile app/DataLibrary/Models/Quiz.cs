using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class Quiz
    {
        public long Id { get; set; }
        public long WaypointId { get; set; }
        public string Question { get; set; }
        public List<QuizAnswer> Answers { get; set; }
    }
}
