using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ainthinai.Service;
using Ainthinai.Service.Model;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/VolunteerLogs")]
    public class VolunteerLogsController : Controller
    {
        private readonly DataContext _context;

        public VolunteerLogsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/VolunteerLogs
        [HttpGet]
        public IEnumerable<VolunteerLog> GetVolunteerLog()
        {
            return _context.VolunteerLog;
        }

        // GET: api/VolunteerLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVolunteerLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerLog = await _context.VolunteerLog.SingleOrDefaultAsync(m => m.Id == id);

            if (volunteerLog == null)
            {
                return NotFound();
            }

            return Ok(volunteerLog);
        }

        // PUT: api/VolunteerLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVolunteerLog([FromRoute] int id, [FromBody] VolunteerLog volunteerLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != volunteerLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(volunteerLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerLogExists(id))
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

        // POST: api/VolunteerLogs
        [HttpPost]
        public async Task<IActionResult> PostVolunteerLog([FromBody] VolunteerLog volunteerLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VolunteerLog.Add(volunteerLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVolunteerLog", new { id = volunteerLog.Id }, volunteerLog);
        }

        // DELETE: api/VolunteerLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVolunteerLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var volunteerLog = await _context.VolunteerLog.SingleOrDefaultAsync(m => m.Id == id);
            if (volunteerLog == null)
            {
                return NotFound();
            }

            _context.VolunteerLog.Remove(volunteerLog);
            await _context.SaveChangesAsync();

            return Ok(volunteerLog);
        }

        private bool VolunteerLogExists(int id)
        {
            return _context.VolunteerLog.Any(e => e.Id == id);
        }
    }
}