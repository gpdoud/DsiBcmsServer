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
    public class CohortsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public CohortsController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: api/Cohorts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cohort>>> GetCohort() {
            return await _context.Cohorts.ToListAsync();
        }

        // GET: api/Cohorts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cohort>> GetCohort(int id) {
            var cohort = await _context.Cohorts.FindAsync(id);

            if(cohort == null) {
                return NotFound();
            }

            return cohort;
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> PostUpdateCohort(int id, Cohort cohort) {
            return await PutCohort(id, cohort);
        }
        // PUT: api/Cohorts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCohort(int id, Cohort cohort) {
            if(id != cohort.Id) {
                return BadRequest();
            }

            cohort.Updated = DateTime.Now;
            _context.Entry(cohort).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!CohortExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cohorts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Cohort>> PostCohort(Cohort cohort) {
            _context.Cohorts.Add(cohort);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCohort", new { id = cohort.Id }, cohort);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Cohort>> PostDeleteCohort(int id) {
            return await DeleteCohort(id);
        }
        // DELETE: api/Cohorts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cohort>> DeleteCohort(int id) {
            var cohort = await _context.Cohorts.FindAsync(id);
            if(cohort == null) {
                return NotFound();
            }

            _context.Cohorts.Remove(cohort);
            await _context.SaveChangesAsync();

            return cohort;
        }

        private bool CohortExists(int id) {
            return _context.Cohorts.Any(e => e.Id == id);
        }
    }
}
