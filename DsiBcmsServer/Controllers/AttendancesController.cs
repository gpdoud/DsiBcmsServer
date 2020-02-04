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
    public class AttendancesController : ControllerBase {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly DsiBcmsContext _context;

        public AttendancesController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: dsi/Attendances/ischeckedin/{cohortid}/{studentId}
        [HttpGet("ischeckedin/{cohortId}/{studentId}")]
        public async Task<ActionResult<Attendance>> IsCheckedIn(int cohortId, int studentId) {
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            var now = DateTime.Now;
            // if attendance is found; student is already checked in
            // if null, not checked in
            return await _context.Attendance
                            .SingleOrDefaultAsync(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null);
        }

        // POST: dsi/Attendances/checkin/{cohortId}/{studendId}
        [HttpPost("checkin/{cohortId}/{studentId}")]
        public async Task<ActionResult<Attendance>> Checkin(int cohortId, int studentId) {
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            var now = DateTime.Now;
            var attendance = await _context.Attendance
                            .SingleOrDefaultAsync(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null);
            // if found; already checked in
            if(attendance != null) return new OkResult();
            // else add it.
            var newAttendance = new Attendance {
                Id = 0,
                In = DateTime.Now,
                Out = null,
                Excused = null,
                Note = null,
                EnrollmentId = enrollment.Id
            };
            return await PostAttendance(newAttendance);
        }

        // POST: dsi/Attendances/checkout/{cohortId}/{studendId}
        [HttpPost("checkout/{cohortId}/{studentId}")]
        public async Task<IActionResult> Checkout(int cohortId, int studentId) {
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            var now = DateTime.Now;
            var attendance = await _context.Attendance
                            .SingleOrDefaultAsync(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null);
            // if not found; error
            if(attendance == null) {
                var ex = new Exception("Checkout without checking");
                logger.Error(ex, "{0}", ex.Message);
                throw ex;
            }
            // check out
            attendance.Out = DateTime.Now;
            return await PutAttendance(attendance.Id, attendance);
        }

        // GET: dsi/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance() {
            try {
                return await _context.Attendance.ToListAsync();
            } catch(Exception ex) {
                var msgs = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions: {msg}", msgs);
                return new JsonResult(msgs);
            }
        }

        // GET: dsi/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id) {
            try {
                var attendance = await _context.Attendance.FindAsync(id);

                if(attendance == null) {
                    return NotFound();
                }

                return attendance;
            } catch(Exception ex) {
                var msgs = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions: {msg}", msgs);
                return new JsonResult(msgs);
            }
        }

        // PUT: dsi/Attendances/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance) {
            try {
                if(id != attendance.Id) {
                    return BadRequest();
                }

                _context.Entry(attendance).State = EntityState.Modified;

                try {
                    await _context.SaveChangesAsync();
                } catch(DbUpdateConcurrencyException) {
                    if(!AttendanceExists(id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }

                return NoContent();
            } catch(Exception ex) {
                var msgs = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions: {msg}", msgs);
                return new JsonResult(msgs);
            }
        }

        // POST: dsi/Attendances
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance) {
            try {
                _context.Attendance.Add(attendance);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
            } catch(Exception ex) {
                var msgs = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions: {msg}", msgs);
                return new JsonResult(msgs);
            }
        }

        // DELETE: dsi/Attendances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendance>> DeleteAttendance(int id) {
            try {
                var attendance = await _context.Attendance.FindAsync(id);
                if(attendance == null) {
                    return NotFound();
                }

                _context.Attendance.Remove(attendance);
                await _context.SaveChangesAsync();

                return attendance;
            } catch(Exception ex) {
                var msgs = Utility.Exceptions.Flatten(ex);
                logger.Error(ex, "Exceptions: {msg}", msgs);
                return new JsonResult(msgs);
            }
        }

        private bool AttendanceExists(int id) {
            return _context.Attendance.Any(e => e.Id == id);
        }
    }
}
