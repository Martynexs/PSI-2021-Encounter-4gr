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
    public class QuizAnswersController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;

        public QuizAnswersController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization)
        {
            _repository = repoWrapper;
            _authorization = authorization;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAnswerDTO>> GetQuiz(long id)
        {
            if(!_repository.QuizAnswer.AnswerExists(id))
            {
                return NotFound();
            }

            var result = await _repository.QuizAnswer.GetQuizAnswerById(id);
            return result.ToDTO();
        }


        [HttpPost]
        public async Task<ActionResult<QuizAnswerDTO>> AddAnswerToQuiz(QuizAnswerDTO answerDTO)
        {
            var result = answerDTO.ToEFModel();

            _repository.QuizAnswer.CreateQuizAnswer(result);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetQuiz), new { id = result.Id }, result.ToDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(long id, QuizAnswerDTO quizAnswer)
        {
            if (id != quizAnswer.Id)
            {
                return BadRequest();
            }

            _repository.QuizAnswer.UpdateAnswer(quizAnswer.ToEFModel());

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.QuizAnswer.AnswerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuizAnswer(long id)
        {
            if (!_repository.QuizAnswer.AnswerExists(id))
            {
                return NotFound();
            }

            await _repository.QuizAnswer.DeleteQuizAnswerById(id);
            await _repository.SaveAsync();
            return NoContent();
        }


    }
}
