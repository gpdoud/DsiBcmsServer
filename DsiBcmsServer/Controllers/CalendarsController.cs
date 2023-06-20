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
    public class CalendarsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public CalendarsController(DsiBcmsContext context) {
            _context = context;
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
