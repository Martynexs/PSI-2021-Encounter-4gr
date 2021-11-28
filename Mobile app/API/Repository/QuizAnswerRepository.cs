using Contracts;
using EncounterAPI.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class QuizAnswerRepository : RepositoryBase<QuizAnswers>, IQuizAnswerRepository
    {
        public QuizAnswerRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateQuizAnswer(QuizAnswers quizAnswer)
        {
            Create(quizAnswer);
        }

        public void DeleteQuizAnswer(QuizAnswers quizAnswer)
        {
            Delete(quizAnswer);
        }

        public async Task<IEnumerable<QuizAnswers>> GetQuizAnswers(long quizId)
        {
            return await FindByCondition(x => x.QuizId == quizId).ToListAsync();
        }

        public void UpdateAnswer(QuizAnswers quizAnswer)
        {
            Update(quizAnswer);
        }
    }
}
