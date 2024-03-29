﻿using System;
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
    public class UsersController : ControllerBase {
        private readonly DsiBcmsContext _context;

        public UsersController(DsiBcmsContext context) {
            _context = context;
        }

        // GET: dsi/getcohort/5
        // Retrieves the active Cohort for a given Student (user)
        [HttpGet("getcohort/{id}")]
        public async Task<IActionResult> GetActiveCohortByStudent(int id) {
            var cohort = from u in _context.Users
                         join e in _context.Enrollments
                            on u.Id equals e.UserId
                         join c in _context.Cohorts
                            on e.CohortId equals c.Id
                         where u.Id == id && c.Active
                         select new { StudentId = u.Id, StudentLastName = u.Lastname, CohortId = c.Id, CohortName = c.Name };
            var result = await cohort.FirstOrDefaultAsync();
            if(result == null) return NotFound();
            return new JsonResult(result);
        }

        // GET: dsi/students
        [HttpGet("students")]
        public async Task<ActionResult<IEnumerable<User>>> GetStudents() {
            try {
                return await _context.Users.Where(x => x.Role.IsStudent).ToListAsync();
            } catch(Exception ex) {
                var message = new { message = ex.Message, exception = ex };
                return new JsonResult(message);
            }
        }

        // GET: dsi/instructors
        [HttpGet("instructors")]
        public async Task<ActionResult<IEnumerable<User>>> GetInstructors() {
            try {
                return await _context.Users.Where(x => x.Role.IsInstructor).ToListAsync();
            } catch (Exception ex) {
                var message = new { message = ex.Message, exception = ex };
                return new JsonResult(message);
            }
        }

        // GET: dsi/login
        [HttpGet("/dsi/login/{username}/{password}")]
        public async Task<ActionResult<User>> Login(string username, string password) {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username.Equals(username) && x.Password.Equals(password));
            if(user == null) return NotFound();
            return user;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser() {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id) {
            var user = await _context.Users.FindAsync(id);

            if(user == null) {
                return NotFound();
            }

            return user;
        }

        // POST: dsi/Users/Update/5
        // To get around problem with Put and Delete
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user) {
            return await PutUser(id, user);
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user) {
            if(id != user.Id) {
                return BadRequest();
            }

            user.Updated = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException) {
                if(!UserExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpPost("delete/{id}")]
        public async Task<ActionResult<User>> PostDeleteUser(int id) {
            return await DeleteUser(id);
        }
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id) {
            var user = await _context.Users.FindAsync(id);
            if(user == null) {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id) {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
