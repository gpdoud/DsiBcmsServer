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
    public class AssessmentsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public AssessmentsController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: dsi/Assessments/bycohort/{cohortId}
        [HttpGet("bycohort/{cohortId}")]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessmentsByCohortId(int cohortId) {
            var assessments = from c in _context.Cohorts
                              join e in _context.Enrollments
                              on c.Id equals e.CohortId
                              join a in _context.Assessments
                              on e.Id equals a.EnrollmentId
                              where c.Id == cohortId
                              select a;
            return await assessments.ToListAsync();
        }

        // GET: dsi/Assessments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments() {
            return await _context.Assessments.ToListAsync();
        }

        // GET: dsi/Assessments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id) {
            var assessment = await _context.Assessments.FindAsync(id);

            if(assessment == null) {
                return NotFound();
            }

            return assessment;
        }

        // PUT: dsi/Assessments/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateAssessment(int id, Assessment assessment) {
            return await PutAssessment(id, assessment);
        }
        // PUT: dsi/Assessments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssessment(int id, Assessment assessment) {
            if(id != assessment.Id) {
                return BadRequest();
            }

            _context.Entry(assessment).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!AssessmentExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: dsi/Assessments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Assessment>> PostAssessment(Assessment assessment) {
            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssessment", new { id = assessment.Id }, assessment);
        }

        // DELETE: dsi/Assessments/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Assessment>> PostDeleteAssessment(int id) {
            return await DeleteAssessment(id);
        }
        // DELETE: dsi/Assessments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Assessment>> DeleteAssessment(int id) {
            var assessment = await _context.Assessments.FindAsync(id);
            if(assessment == null) {
                return NotFound();
            }

            _context.Assessments.Remove(assessment);
            await _context.SaveChangesAsync();

            return assessment;
        }

        private bool AssessmentExists(int id) {
            return _context.Assessments.Any(e => e.Id == id);
        }
    }
}
