using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IQuizAnswerRepository : IRepositoryBase<QuizAnswers>
    {
        Task<IEnumerable<QuizAnswers>> GetQuizAnswers(long quizId);
        Task<QuizAnswers> GetQuizAnswerById(long id);
        void CreateQuizAnswer(QuizAnswers quizAnswer);
        void UpdateAnswer(QuizAnswers quizAnswer);
        void DeleteQuizAnswer(QuizAnswers quizAnswer);
        Task DeleteQuizAnswerById(long id);
        Task DeleteQuizAnswersByQuizId(long quizId);
        bool AnswerExists(long id);
    }
}
