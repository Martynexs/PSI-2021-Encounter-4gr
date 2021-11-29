using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IQuizRepository : IRepositoryBase<Quiz>
    {
        Task<IEnumerable<Quiz>> GetAllQuizes();
        Task<Quiz> GetQuizByWaypointId(long waypointId);
        Task<Quiz> GetQuizById(long Id);
        void CreateQuiz(Quiz quiz);
        void UpdateQuiz(Quiz quiz);
        void DeleteQuiz(Quiz quiz);
        Task DeleteById(long id);
        bool QuizExists(long id);
    }
}
