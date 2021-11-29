using EncounterAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Quiz
    {
        public long Id { get; set; }
        public virtual Waypoint Wayoint { get; set; }
        [Required]
        public long WaypointId { get; set; }
        [Required]
        public string Question { get; set; }
        public virtual List<QuizAnswers> Answers { get; set; }
    }
}
