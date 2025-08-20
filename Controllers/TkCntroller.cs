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
        public async Task<ActionResult> Update(int id, NomTask updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.AssignedId = updatedTask.AssignedId;
            task.DueDate = updatedTask.DueDate;

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
                .ToListAsync();
            return expiredTasks;
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<NomTask>>> GetActiveTasks()
        {
            var today = DateTime.Today;
            var activeTasks = await _context.Tasks
                .Where(t => t.DueDate >= today)
                .ToListAsync();
            return activeTasks;
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<NomTask>>> GetTasksByDate(DateTime date)
        {
            var tasksByDate = await _context.Tasks
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();
            return tasksByDate;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<NomTask>>>
GetTasksByUser(string userId)
        {
            var taskByUser = await _context.Tasks
            .Where(t => (t.AssignedId ??"")==userId)
            .ToListAsync()
            ; return tasksByUser;
        
    }      
    }
}

