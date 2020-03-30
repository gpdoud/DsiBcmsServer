using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Controllers {
    [Route("dsi/[controller]")]
    [ApiController]
    public class EvaluationsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public EvaluationsController(DsiBcmsContext context) {
            _context = context;
        }

        // POST: dsi/Evaluations/Assign/5/9
        [HttpPost("assign/{evaluationId}/{cohortId}")]
        public async Task<IActionResult> AssignToEnrollment(int evaluationId, int cohortId) {
            var evaluation = await _context.Evaluations.FindAsync(evaluationId);
            var enrollmentIds = await _context.Enrollments.Where(x => x.CohortId == cohortId).Select(x => x.Id).ToListAsync();
            var evals_created = 0;
            foreach(var enrollmentId in enrollmentIds) {
                // copy the evaluation
                var eval = new Evaluation(evaluation, enrollmentId);
                await _context.Evaluations.AddAsync(eval);
                await _context.SaveChangesAsync();
                // copy the questions
                foreach(var question in evaluation.Questions) {
                    var quest = new Question(question, eval.Id);
                    var newQuest = await _context.Questions.AddAsync(quest);
                }
                await _context.SaveChangesAsync();
                evals_created++;
            }
            var msg = new { evals_created };
            return new OkObjectResult(msg);
        }

        // GET: dsi/Evaluations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluations() {
            return await _context.Evaluations.ToListAsync();
        }

        // GET: api/Evaluations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(int id) {
            var evaluation = await _context.Evaluations.FindAsync(id);

            if(evaluation == null) {
                return NotFound();
            }

            return evaluation;
        }

        // POST: dsi/Evaluations/Update/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateEvaluation(int id, Evaluation evaluation) {
            return await PutEvaluation(id, evaluation);
        }

        // PUT: dsi/Evaluations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(int id, Evaluation evaluation) {
            if(id != evaluation.Id) {
                return BadRequest();
            }

            evaluation.Updated = Utility.Date.EasternTimeNow;
            _context.Entry(evaluation).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!EvaluationExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Evaluations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation) {
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluation", new { id = evaluation.Id }, evaluation);
        }

        // DELETE: dsi/Evaluations/Delete/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Evaluation>> RemoveEvaluation(int id) {
            return await DeleteEvaluation(id);
        }

        // DELETE: api/Evaluations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evaluation>> DeleteEvaluation(int id) {
            var evaluation = await _context.Evaluations.FindAsync(id);
            if(evaluation == null) {
                return NotFound();
            }

            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();

            return evaluation;
        }

        private bool EvaluationExists(int id) {
            return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}
