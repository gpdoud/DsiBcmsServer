using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Controllers
{
    [Route("dsi/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public QuestionsController(DsiBcmsContext context) {
            _context = context;
        }

        private async Task<IActionResult> RecalcPoints(int evaluationId) {
            var e = await _context.Evaluations.FindAsync(evaluationId);
            e.PointsAvailable = _context.Questions
                .Where(q => q.EvaluationId == evaluationId)
                .Sum(q => q.PointValue);
            e.PointsScored = _context.Questions
                .Where(q => q.EvaluationId == evaluationId && q.CorrectAnswerNbr == q.UserAnswerNbr)
                .Sum(q => q.PointValue); ;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions() {
            return await _context.Questions.ToListAsync();
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id) {
            var Question = await _context.Questions.FindAsync(id);

            if(Question == null) {
                return NotFound();
            }

            return Question;
        }

        // POST: api/Questions/Update/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, Question question) {
            return await PutQuestion(id, question);
        }

        // PUT: api/Questions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            question.Updated = Utility.Date.EasternTimeNow;
            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            await RecalcPoints(question.EvaluationId);
            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            await RecalcPoints(question.EvaluationId);
            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // POST: api/Questions/Delete/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Question>> RemoveQuestion(int id) {
            return await DeleteQuestion(id);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            await RecalcPoints(question.EvaluationId);
            return question;
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
