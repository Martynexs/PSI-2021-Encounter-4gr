using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
