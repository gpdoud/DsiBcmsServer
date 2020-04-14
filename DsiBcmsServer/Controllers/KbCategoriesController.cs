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
    public class KbCategoriesController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public KbCategoriesController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: api/KbCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KbCategory>>> GetKbCategories() {
            return await _context.KbCategories.ToListAsync();
        }

        // GET: api/KbCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KbCategory>> GetKbCategory(int id) {
            var kbCategory = await _context.KbCategories.FindAsync(id);

            if(kbCategory == null) {
                return NotFound();
            }

            return kbCategory;
        }

        // POST: dsi/KbCategories/update/5
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateKbCategory(int id, KbCategory kbCategory) {
            return await PutKbCategory(id, kbCategory);
        }

        // PUT: api/KbCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKbCategory(int id, KbCategory kbCategory) {
            if(id != kbCategory.Id) {
                return BadRequest();
            }

            kbCategory.Updated = Utility.Date.EasternTimeNow;
            _context.Entry(kbCategory).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!KbCategoryExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KbCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<KbCategory>> PostKbCategory(KbCategory kbCategory) {
            _context.KbCategories.Add(kbCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKbCategory", new { id = kbCategory.Id }, kbCategory);
        }

        // POST: api/KbCategories/delete/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<KbCategory>> RemoveKbCategory(int id) {
            return await DeleteKbCategory(id);
        }

        // DELETE: api/KbCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KbCategory>> DeleteKbCategory(int id) {
            var kbCategory = await _context.KbCategories.FindAsync(id);
            if(kbCategory == null) {
                return NotFound();
            }

            _context.KbCategories.Remove(kbCategory);
            await _context.SaveChangesAsync();

            return kbCategory;
        }

        private bool KbCategoryExists(int id) {
            return _context.KbCategories.Any(e => e.Id == id);
        }
    }
}
