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
        void CreateQuizAnswer(QuizAnswers quizAnswer);
        void UpdateAnswer(QuizAnswers quizAnswer);
        void DeleteQuizAnswer(QuizAnswers quizAnswer);
    }
}
