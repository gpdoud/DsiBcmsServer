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
    public class KbsController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public KbsController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: api/Kbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kb>>> GetKbs() {
            return await _context.Kbs.ToListAsync();
        }

        // GET: api/Kbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kb>> GetKb(int id) {
            var kb = await _context.Kbs.FindAsync(id);

            if(kb == null) {
                return NotFound();
            }

            return kb;
        }

        // POST: api/Kbs/update/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateKb(int id, Kb kb) {
            return await PutKb(id, kb);
        }

        // PUT: api/Kbs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKb(int id, Kb kb) {
            if(id != kb.Id) {
                return BadRequest();
            }

            kb.Updated = Utility.Date.EasternTimeNow;
            _context.Entry(kb).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!KbExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Kbs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Kb>> PostKb(Kb kb) {
            _context.Kbs.Add(kb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKb", new { id = kb.Id }, kb);
        }

        // POST: api/Kbs/delete/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<Kb>> PostDeleteKb(int id) {
            return await DeleteKb(id);
        }

        // DELETE: api/Kbs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Kb>> DeleteKb(int id) {
            var kb = await _context.Kbs.FindAsync(id);
            if(kb == null) {
                return NotFound();
            }

            _context.Kbs.Remove(kb);
            await _context.SaveChangesAsync();

            return kb;
        }

        private bool KbExists(int id) {
            return _context.Kbs.Any(e => e.Id == id);
        }
    }
}
