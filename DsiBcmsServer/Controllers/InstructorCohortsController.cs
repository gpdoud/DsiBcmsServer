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
    public class InstructorCohortsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public InstructorCohortsController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: api/InstructorCohorts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorCohort>>> GetInstructorCohort() {
            return await _context.InstructorCohorts
                                    .Include(x => x.Instructor)
                                    .ToListAsync();
        }

        // GET: api/InstructorCohorts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorCohort>> GetInstructorCohort(int id) {
            var instructorCohort = await _context.InstructorCohorts
                                                    .Include(x => x.Instructor)
                                                    .SingleOrDefaultAsync(x => x.Id == id);

            if (instructorCohort == null) {
                return NotFound();
            }

            return instructorCohort;
        }

        // PUT: api/InstructorCohorts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("update/{id}")]
        public async Task<IActionResult> PutInstructorCohort(int id, InstructorCohort instructorCohort) {
            if (id != instructorCohort.Id) {
                return BadRequest();
            }

            _context.Entry(instructorCohort).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!InstructorCohortExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InstructorCohorts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InstructorCohort>> PostInstructorCohort(InstructorCohort instructorCohort) {
            _context.InstructorCohorts.Add(instructorCohort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstructorCohort", new { id = instructorCohort.Id }, instructorCohort);
        }

        // DELETE: api/InstructorCohorts/5
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteInstructorCohort(int id) {
            var instructorCohort = await _context.InstructorCohorts.FindAsync(id);
            if (instructorCohort == null) {
                return NotFound();
            }

            _context.InstructorCohorts.Remove(instructorCohort);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstructorCohortExists(int id) {
            return _context.InstructorCohorts.Any(e => e.Id == id);
        }
    }
}
