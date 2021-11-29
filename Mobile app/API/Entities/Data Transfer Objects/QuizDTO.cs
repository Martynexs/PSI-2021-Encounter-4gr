using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Data_Transfer_Objects
{
    public class QuizDTO
    {
        public long Id { get; set; }
        public long WaypointId { get; set; }
        public string Question { get; set; }
        public List<QuizAnswerDTO> Answers { get; set; }
    }
}
