using Contracts;
using Entities.Data_Transfer_Objects;
using Entities.TypeExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;

        public QuizzesController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization)
        {
            _repository = repoWrapper;
            _authorization = authorization;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuizById(long id)
        {
            var quiz = await _repository.Quiz.GetQuizById(id);
            if (quiz == default)
            {
                return NotFound();
            }
            var answers = await _repository.QuizAnswer.GetQuizAnswers(quiz.Id);
            var result = quiz.ToDTO();
            result.Answers = answers.Select(x => x.ToDTO()).ToList();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<QuizDTO>> CreateQuiz(QuizDTO quiz)
        {
            var quizModel = quiz.ToEFModel();

            _repository.Quiz.CreateQuiz(quizModel);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetQuizById), new { id = quizModel.Id }, quizModel.ToDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateQuiz(long id, QuizDTO quiz)
        {
            if (quiz.Answers == null || quiz.Id != id)
            {
                return BadRequest();
            }

            if(!_repository.Quiz.QuizExists(id))
            {
                return NotFound();
            }

            await _repository.QuizAnswer.DeleteQuizAnswersByQuizId(quiz.Id);

            var quizModel = quiz.ToEFModel();
            _repository.Quiz.UpdateQuiz(quizModel);

            var quizAnswers = quiz.Answers.Select(x => x.ToEFModel());
            foreach (var q in quizAnswers)
            {
                q.QuizId = quizModel.Id;
                _repository.QuizAnswer.CreateQuizAnswer(q);
            }
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(long id)
        {
            await _repository.Quiz.DeleteById(id);
            await _repository.SaveAsync();
            return NoContent();
        }

    }
}
