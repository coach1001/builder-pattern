using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreDuiWebApi.Authentication.Data;
using CoreDuiWebApi.Data;

namespace CoreDuiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbUsersController : ControllerBase
    {
        private readonly DbLabCalcContext _context;

        public DbUsersController(DbLabCalcContext context)
        {
            _context = context;
        }

        // GET: api/DbUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbUser>>> GetDbUsers()
        {
            return await _context.DbUsers.ToListAsync();
        }

        // GET: api/DbUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DbUser>> GetDbUser(long id)
        {
            var dbUser = await _context.DbUsers.FindAsync(id);

            if (dbUser == null)
            {
                return NotFound();
            }

            return dbUser;
        }

        // PUT: api/DbUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDbUser(Guid id, DbUser dbUser)
        {
            if (id != dbUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(dbUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbUserExists(id))
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

        // POST: api/DbUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DbUser>> PostDbUser(DbUser dbUser)
        {
            _context.DbUsers.Add(dbUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDbUser", new { id = dbUser.Id }, dbUser);
        }

        // DELETE: api/DbUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DbUser>> DeleteDbUser(long id)
        {
            var dbUser = await _context.DbUsers.FindAsync(id);
            if (dbUser == null)
            {
                return NotFound();
            }

            _context.DbUsers.Remove(dbUser);
            await _context.SaveChangesAsync();

            return dbUser;
        }

        private bool DbUserExists(Guid id)
        {
            return _context.DbUsers.Any(e => e.Id == id);
        }
    }
}
