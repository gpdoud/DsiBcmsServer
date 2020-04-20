using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSI.BcmsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSI.BcmsServer.Controllers {

    [Route("dsi/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase {

        private readonly DsiBcmsContext _context;

        public LogsController(DsiBcmsContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetAll() {
            return await _context.Logs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> Get(int id) {
            var log = await _context.Logs.FindAsync(id);
            if(log == null) return NotFound();
            return log;
        }

        private async Task<IActionResult> Log(Log log) {
            log.Timestamp = Utility.Date.EasternTimeNow;
            _context.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = log.Id }, log);
        }

        [HttpPost]
        public async Task<IActionResult> LogCreate(Log log) {
            return await Log(log);
        }

        [HttpPost("info")]
        public async Task<IActionResult> LogInfo(Log log) {
            log.Severity = LogSeverity.Info;
            return await Log(log);
        }

        [HttpPost("warn")]
        public async Task<IActionResult> LogWarning(Log log) {
            log.Severity = LogSeverity.Warn;
            return await Log(log);
        }

        [HttpPost("error")]
        public async Task<IActionResult> LogError(Log log) {
            log.Severity = LogSeverity.Error;
            return await Log(log);
        }

        [HttpPost("fatal")]
        public async Task<IActionResult> LogFatal(Log log) {
            log.Severity = LogSeverity.Fatal;
            return await Log(log);
        }

        // POST: api/Logs/Update/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateKb(int id, Log log) {
            return await PutKb(id, log);
        }

        // PUT: api/Logs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKb(int id, Log log) {
            if(id != log.Id) {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!LogExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Kbs/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Log>> RemoveLog(int id) {
            return await DeleteLog(id);
        }

        // DELETE: api/Kbs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Log>> DeleteLog(int id) {
            var log = await _context.Logs.FindAsync(id);
            if(log == null) {
                return NotFound();
            }

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();

            return log;
        }

        private bool LogExists(int id) {
            return _context.Logs.Any(e => e.Id == id);
        }
    }
}