using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DSI.BcmsServer.Models;

namespace DSI.BcmsServer.Controllers
{
    [Route("dsi/[controller]")]
    [ApiController]
    public class CommentariesController : ControllerBase
    {
        private readonly DsiBcmsContext _context;

        public CommentariesController(DsiBcmsContext context)
        {
            _context = context;
        }

        // GET: api/Commentaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commentary>>> GetCommentary()
        {
            return await _context.Commentary.ToListAsync();
        }

        // GET: api/Commentaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commentary>> GetCommentary(int id)
        {
            var commentary = await _context.Commentary.FindAsync(id);

            if (commentary == null)
            {
                return NotFound();
            }

            return commentary;
        }

        // PUT: api/Commentaries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentary(int id, Commentary commentary)
        {
            if (id != commentary.Id)
            {
                return BadRequest();
            }

            _context.Entry(commentary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Commentaries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Commentary>> PostCommentary(Commentary commentary)
        {
            _context.Commentary.Add(commentary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentary", new { id = commentary.Id }, commentary);
        }

        // DELETE: api/Commentaries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commentary>> DeleteCommentary(int id)
        {
            var commentary = await _context.Commentary.FindAsync(id);
            if (commentary == null)
            {
                return NotFound();
            }

            _context.Commentary.Remove(commentary);
            await _context.SaveChangesAsync();

            return commentary;
        }

        private bool CommentaryExists(int id)
        {
            return _context.Commentary.Any(e => e.Id == id);
        }
    }
}
