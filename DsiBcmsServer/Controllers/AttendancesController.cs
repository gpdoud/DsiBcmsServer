using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;
using DSI.BcmsServer.Utility;
using DSI.BcmsServer.ModelViews;

namespace DSI.BcmsServer.Controllers {
    [Route("dsi/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase {

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly DsiBcmsContext _context;

        public AttendancesController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: dsi/Attendances/report/{cohortId}
        [HttpGet("report/{cohortId}")]
        public async Task<ActionResult<IEnumerable<AttendanceReport>>> GetAttendanceForCohort(int cohortId) {
            var students = (from e in _context.Enrollments
                            join u in _context.Users
                            on e.UserId equals u.Id
                            where e.CohortId == cohortId
                            orderby u.Lastname
                            select u);

            var reports = new List<AttendanceReport>();

            foreach(var student in await students.ToListAsync()) {

                var attendances = from c in _context.Cohorts
                                  join e in _context.Enrollments
                                  on c.Id equals e.CohortId
                                  join a in _context.Attendance
                                  on e.Id equals a.EnrollmentId
                                  join u in _context.Users
                                  on e.UserId equals u.Id
                                  where c.Id == cohortId
                                    && u.Id == student.Id
                                  orderby a.In
                                  select a;

                var report = new AttendanceReport {
                    Student = student,
                    Attendances = await attendances.ToListAsync()
                };

                reports.Add(report);

            }

            return reports;
        }

        // GET: dsi/Attendances/ischeckedin/{cohortid}/{studentId}
        [HttpGet("ischeckedin/{cohortId}/{studentId}")]
        public async Task<ActionResult<Attendance>> IsCheckedIn(int cohortId, int studentId) {
            logger.Trace("Attendance.IsCheckedIn: parms cohortId({0}), studentId({1})", cohortId, studentId);
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            var now = Date.EasternTimeNow;
            // if attendance is found; student is already checked in
            // if null, not checked in
            /*
             * Bugfix for multiple checkins without a checkout
             * Retrieve all attendance records for the day
             * If there are none, no checkin has been done
             * If there is 1, a normal checkin has been done
             * If there are more than 1, this is an error 
             * - log the error
             * - return the first one
             */
            var attnds = await _context.Attendance
                            .Where(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null)
                            .ToListAsync();
            if(attnds.Count > 1) {
                var attnd = attnds[0];
                _context.Logs.Add(new Models.Log() {
                    Id = 0,
                    Message = $"ERROR: More than 1 checking for " +
                                $"student {studentId} on date {attnd.In.Month}/{attnd.In.Day}/{attnd.In.Year} ",
                    Severity = LogSeverity.Error,
                    Timestamp = Utility.Date.EasternTimeNow
                });
                await _context.SaveChangesAsync();
                await RemoveDuplicateCheckins(attnds);
            }
            // if there are no attendance records, return null
            // if there are 1 or more, return the first one
            return attnds.Count == 0 ? null : attnds[0];

        }

        private async Task RemoveDuplicateCheckins(List<Attendance> attendances) {
            // skip the first one
            for(var idx = 1; idx < attendances.Count; idx++) {
                _context.Attendance.Remove(attendances[idx]);
            }
            await _context.SaveChangesAsync();
        }

        // POST: dsi/Attendances/checkin/{cohortId}/{studendId}
        [HttpPost("checkin/{cohortId}/{studentId}")]
        public async Task<ActionResult<Attendance>> Checkin(int cohortId, int studentId, Attendance attnd) {
            logger.Trace("Attendance.CheckedIn: parms cohortId({0}), studentId({1})", cohortId, studentId);
            var student = await _context.Users.FindAsync(studentId);
            logger.Debug("CheckIn(): Checking in {0} {1}", student.Firstname, student.Lastname);
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            //var now = DateTime.Now;
            var now = Date.EasternTimeNow;
            var attendance = await _context.Attendance
                            .SingleOrDefaultAsync(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null);
            // if found; already checked in
            logger.Debug("CheckIn(): Is {0} {1} already checked in? {2}", student.Firstname, student.Lastname, (attendance != null ? "Yes" : "No"));
            if(attendance != null) return new OkResult();
            // else add it.
            var newAttendance = new Attendance {
                Id = 0,
                In = Date.EasternTimeNow,
                Out = null,
                Excused = attnd.Excused,
                Absent = attnd.Absent,
                Note = attnd.Note,
                SecureNote = attnd.SecureNote,
                EnrollmentId = enrollment.Id
            };
            logger.Trace("Checking-in studentId {0} at {1}", studentId, newAttendance.In);
            return await PostAttendance(newAttendance);
        }

        // POST: dsi/Attendances/checkout/{cohortId}/{studendId}
        [HttpPost("checkout/{cohortId}/{studentId}")]
        public async Task<IActionResult> Checkout(int cohortId, int studentId, Attendance attnd) {
            logger.Trace("Attendance.CheckedOut: parms cohortId({0}), studentId({1})", cohortId, studentId);
            var student = await _context.Users.FindAsync(studentId);
            logger.Debug("CheckOut(): Checking out {0} {1}", student.Firstname, student.Lastname);
            var enrollment = await _context.Enrollments
                             .SingleOrDefaultAsync(x => x.CohortId == cohortId && x.UserId == studentId);
            if(enrollment == null) return NotFound();
            var now = Date.EasternTimeNow;
            var attendance = await _context.Attendance
                            .SingleOrDefaultAsync(x => x.EnrollmentId == enrollment.Id
                                && x.In.Year == now.Year && x.In.Month == now.Month && x.In.Day == now.Day
                                && x.Out == null);
            // if not found; error
            if(attendance == null) {
                var ex = new Exception("Checkout without checkin");
                logger.Error(ex, "{0}", ex.Message);
                throw ex;
            }
            // check out
            attendance.Out = Date.EasternTimeNow;
            if(attnd.Excused != null) {
                attendance.Excused = attnd.Excused;
            }
            if(attnd.Absent != null) {
                attendance.Absent = attnd.Absent;
            }
            if(attnd.Note != null && attnd.Note.Trim().Length > 0) { // append note if exists
                attendance.Note += $" || {attnd.Note}";
            }
            logger.Trace("Checking-out student {0} {1} at {2}", student.Firstname, student.Lastname, attendance.Out);
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

        [HttpPost("update/{id}")]
        public async Task<IActionResult> PostUpdateAttendance(int id, Attendance attendance) {
            return await PutAttendance(id, attendance);
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

        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Attendance>> PostDeleteAttendance(int id) {
            return await DeleteAttendance(id);
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
