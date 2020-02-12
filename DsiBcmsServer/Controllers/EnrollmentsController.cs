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
    public class EnrollmentsController : ControllerBase {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly DsiBcmsContext _context;

        public EnrollmentsController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollment() {
            try {
                return await _context.Enrollments.ToListAsync();
            } catch(Exception ex) {
                return new JsonResult(ex.Message, ex);
            }
        }

        // GET: api/Enrollments/5
        [HttpGet("{userId}/{cohortId}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int userId, int cohortId) {
            try {
                var enrollment = await _context.Enrollments.FindAsync(userId, cohortId);

                if(enrollment == null) {
                    return NotFound();
                }

                return enrollment;
            } catch(Exception ex) {
                return new JsonResult(ex.Message, ex);
            }

        }

        // GET: api/Enrollments/5
        [HttpGet("notenrolled/{cohortId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetNotEnrolledByCohort(int cohortId) {
            try {
                var sql = $" exec GetUsersNotEnrolled @Cohortid = {cohortId} ";
                return await _context.Users
                                    .FromSqlRaw(sql)
                                    .ToListAsync();
                //return await _context.Users.Where(u => u.Role.IsStudent && _context.Enrollments.All(e => e.UserId != u.Id)).ToListAsync();
            } catch(Exception ex) {
                var exceptionMessages = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions:{msg}", exceptionMessages);
                return new JsonResult(ex.Message, ex);
            }
        }

        [HttpPost("update/{userId}/{cohortId}")]
        public async Task<IActionResult> PostUpdateEnrollment(int id, Enrollment enrollment) {
            return await PutEnrollment(id, enrollment);
        }
        // PUT: api/Enrollments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{userId}/{cohortId}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment) {
            try {
                if(id != enrollment.Id) {
                    return BadRequest();
                }

                _context.Entry(enrollment).State = EntityState.Modified;

                try {
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!EnrollmentExists(id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return NoContent();
            } catch(Exception ex) {
                return new JsonResult(ex.Message, ex);
            }

        }

        // POST: api/Enrollments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment) {
            try {
                _context.Enrollments.Add(enrollment);
                try {
                    await _context.SaveChangesAsync();
                } catch(DbUpdateException) {
                    if(EnrollmentExists(enrollment.Id)) {
                        return Conflict();
                    } else {
                        throw;
                    }
                }

                return CreatedAtAction("GetEnrollment", new { userId = enrollment.UserId, cohortId = enrollment.CohortId }, enrollment);
            } catch(Exception ex) {
                return new JsonResult(ex.Message, ex);
            }

        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Enrollment>> PostDeleteEnrollment(int id) {
            return await DeleteEnrollment(id);
        }
        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Enrollment>> DeleteEnrollment(int id) {
            try {
                var enrollment = await _context.Enrollments.FindAsync(id);
                if(enrollment == null) {
                    return NotFound();
                }

                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                return enrollment;
            } catch(Exception ex) {
                return new JsonResult(ex.Message, ex);
            }

        }

        private bool EnrollmentExists(int id) {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}
