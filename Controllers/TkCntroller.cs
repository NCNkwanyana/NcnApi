using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NcnApi.Models;
using NcnApi.Data;

namespace NcnApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TkController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<NomTask>>> GetAll() => await _context.Tasks.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<NomTask>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<NomTask>> Create(NomTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, NomTask updated)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = updated.Title;
            task.Description = updated.Description;
            task.AssignedId = updated.AssignedId;
            task.DueDate = updated.DueDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("expired")]
        public async Task<ActionResult<List<NomTask>>> GetExpiredTasks()
        {
            var today = DateTime.Today;
            var expiredTasks = await _context.Tasks
                .Where(t => t.DueDate < today)
                .ToListAsync();return expiredTasks;

        }

        [HttpGet("active")]
        public async Task<ActionResult<List<NomTask>>> GetActiveTasks()
        {
            var today = DateTime.Today;
            return await _context.Tasks
                .Where(t => t.DueDate >= today)
                .ToListAsync();
            
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<NomTask>>> GetTasksByDate(DateTime date)
        {
            var d = date.Date;
                return await _context.Tasks
                .Where(t => t.DueDate.Date == d)
                .ToListAsync();
          
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<NomTask>>>
GetTasksByUser(string userId)
        {
          return await _context.Tasks
            .Where(t => t.AssignedId != null && t.AssignedId.Equals(userId))
            .ToListAsync()
            ; 
        
    }      
    }
}

