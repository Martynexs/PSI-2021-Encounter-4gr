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

        public async Task DeleteQuizAnswerById(long id)
        {
            var answer = await GetQuizAnswerById(id);
            Delete(answer);
        }

        public async Task DeleteQuizAnswersByQuizId(long quizId)
        {
            var answers = await FindByCondition(x => x.QuizId == quizId).ToListAsync();
            foreach(var a in answers)
            {
                Delete(a);
            }
        }

        public async Task<QuizAnswers> GetQuizAnswerById(long id)
        {
            return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<QuizAnswers>> GetQuizAnswers(long quizId)
        {
            return await FindByCondition(x => x.QuizId == quizId).ToListAsync();
        }

        public void UpdateAnswer(QuizAnswers quizAnswer)
        {
            Update(quizAnswer);
        }

        public bool AnswerExists(long id)
        {
            return FindByCondition(x => x.Id == id).Any();
        }
    }
}
