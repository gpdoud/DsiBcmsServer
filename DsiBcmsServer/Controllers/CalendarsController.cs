using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace DSI.BcmsServer.Controllers {
    [Route("dsi/[controller]")]
    [ApiController]
    public class CalendarsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public CalendarsController(DsiBcmsContext context) {
            _context = context;
        }

        // POST: api/calendars/clone/5/2023-08-27
        [HttpPost("clone/{srcCalendarId}/{startDateStr}")]
        public async Task<ActionResult<Calendar>> CloneCalendar(int srcCalendarId, string startDateStr) {
            if(srcCalendarId == 0 || startDateStr == null) {
                return BadRequest();
            }
            var srcCalendar = await _context.Calendars
                                                .Include(x => x.CalendarDays)
                                                .SingleOrDefaultAsync(x => x.Id == srcCalendarId);
            if(srcCalendar == null) {
                return NotFound();
            }
            DateTime startDate;
            if (!DateTime.TryParse(startDateStr, out startDate)) {
                throw new ArgumentException("StartDate cannot be parsed!");
            }
 
            var newCalendar = await CloneCalendar(srcCalendar, startDate);
            await CloneCalendarDays(srcCalendar, newCalendar);
            return newCalendar;
        }

        private async Task<Calendar> CloneCalendar(Calendar srcCalendar, DateTime startDate) {
            var newCalendar = srcCalendar.Clone();
            newCalendar.StartDate = startDate;
            newCalendar.Description = srcCalendar.Description + " CLONED";
            _context.Calendars.Add(newCalendar);
            await _context.SaveChangesAsync();
            return newCalendar;
        }
        
        private async Task CloneCalendarDays(Calendar fromCalendar, Calendar toCalendar) {
            var nextDate = (DateTime) toCalendar.StartDate;
            foreach(var day in fromCalendar.CalendarDays) {
                var newday = day.Clone();
                newday.CalendarId = toCalendar.Id;
                newday.Date = nextDate;

                _context.CalendarDays.Add(newday);
                await _context.SaveChangesAsync();
                nextDate = toCalendar.Type switch {
                    Calendar.CalendarType_Fulltime => GetNextFullTimeDate(nextDate),
                    Calendar.CalendarType_Parttime => GetNextPartTimeDate(nextDate),
                    _ => throw new Exception("Calendar type is not FT or PT!")
                };
                    
            }
            return;
        }

        private DateTime GetNextFullTimeDate(DateTime date) {
            var nextDate = date.AddDays(1);
            // if the next day is a Saturday
            if(nextDate.DayOfWeek == DayOfWeek.Saturday) {
                // add 2 more days to make it Monday
                return nextDate.AddDays(2);
            }
            return nextDate;
        }        
        
        private DateTime GetNextPartTimeDate(DateTime date) {
            var nextDate = date.AddDays(1);
            // if the next day is Sunday
            if(nextDate.DayOfWeek == DayOfWeek.Sunday
                || nextDate.DayOfWeek == DayOfWeek.Tuesday) {
                // add 1 more day to make it Monday
                return nextDate.AddDays(1);
            } else if(nextDate.DayOfWeek == DayOfWeek.Thursday) {
                return nextDate.AddDays(2);
            }
            return nextDate;
        }

        // GET: api/Calendars/userId
        [HttpGet("student/{userId}")]
        public async Task<ActionResult<Calendar>> GetCalendarForStudentId(int userId) {
            var calendar = from u in _context.Users
                           join e in _context.Enrollments
                               on u.Id equals e.UserId
                           join c in _context.Cohorts
                               on e.CohortId equals c.Id
                           join cal in _context.Calendars
                               on c.CalendarId equals cal.Id
                           where u.Id == userId
                           select cal;
            
            return await calendar.FirstOrDefaultAsync();

        }


        // GET: api/Calendars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetCalendars() {
            return await _context.Calendars
                                    .Include(x => x.CalendarDays)
                                    .ToListAsync();
        }

        // GET: api/Calendars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calendar>> GetCalendar(int id) {
            var calendar = await _context.Calendars
                                            .Include(x => x.CalendarDays)
                                            .SingleOrDefaultAsync(x => x.Id == id);

            if (calendar == null) {
                return NotFound();
            }

            return calendar;
        }

        // PUT: api/Calendars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendar(int id, Calendar calendar) {
            if (id != calendar.Id) {
                return BadRequest();
            }

            calendar.Updated = Utility.Date.EasternTimeNow;
            _context.Entry(calendar).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!CalendarExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateCalendar(int id, Calendar calendar) {
            return await PutCalendar(id, calendar);
        }
            // POST: api/Calendars
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(Calendar calendar) {
            calendar.Created = Utility.Date.EasternTimeNow;
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendar", new { id = calendar.Id }, calendar);
        }

        // DELETE: api/Calendars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id) {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null) {
                return NotFound();
            }

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteCalendar(int id, Calendar calendar) {
            return await DeleteCalendar(id);
        }

        private bool CalendarExists(int id) {
            return _context.Calendars.Any(e => e.Id == id);
        }
    }
}
