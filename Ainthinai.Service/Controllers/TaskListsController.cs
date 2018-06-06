using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ainthinai.Service.Model;
using Ainthinai.Service.Models;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/TaskLists")]
    public class TaskListsController : Controller
    {
        private readonly DataContext _context;

        public TaskListsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TaskLists
        [HttpGet]
        public IEnumerable<TaskList> GetTaskList()
        {
            return _context.TaskList;
        }

        // GET: api/TaskLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.Id == id);

            if (taskList == null)
            {
                return NotFound();
            }

            return Ok(taskList);
        }

        // PUT: api/TaskLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskList([FromRoute] int id, [FromBody] TaskList taskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != taskList.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskListExists(id))
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

        // POST: api/TaskLists
        [HttpPost]
        public async Task<IActionResult> PostTaskList([FromBody] TaskList taskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TaskList.Add(taskList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskList", new { id = taskList.Id }, taskList);
        }

        // DELETE: api/TaskLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskList([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.Id == id);
            if (taskList == null)
            {
                return NotFound();
            }

            _context.TaskList.Remove(taskList);
            await _context.SaveChangesAsync();

            return Ok(taskList);
        }

        private bool TaskListExists(int id)
        {
            return _context.TaskList.Any(e => e.Id == id);
        }
    }
}