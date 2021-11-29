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
    public class QuizRepository : RepositoryBase<Quiz>, IQuizRepository
    {
        public QuizRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateQuiz(Quiz quiz)
        {
            Create(quiz);
        }

        public void DeleteQuiz(Quiz quiz)
        {
            Delete(quiz);
        }

        public async Task DeleteById(long id)
        {
            var quiz = await GetQuizById(id);
            Delete(quiz);
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizes()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Quiz> GetQuizById(long Id)
        {
            return await FindByCondition(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Quiz> GetQuizByWaypointId(long waypointId)
        {
            return await FindByCondition(x => x.WaypointId == waypointId).FirstOrDefaultAsync();
        }

        public void UpdateQuiz(Quiz quiz)
        {
            Update(quiz);
        }

        public bool QuizExists(long id)
        {
            return FindByCondition(x => x.Id == id).Any();
        }
    }
}
