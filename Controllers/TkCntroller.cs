using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NcnApi.Data;
using NcnApi.Models;

namespace NcnApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT required for all endpoints
    public class TkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tk
        [HttpGet]
        public async Task<ActionResult<List<NomTask>>> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks;
        }

        // GET: api/tk/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<NomTask>> GetById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task is null ? NotFound() : task;
        }

        // POST: api/tk
        [HttpPost]
        public async Task<ActionResult<NomTask>> Create(NomTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT: api/tk/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, NomTask updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.AssignedId = updatedTask.AssignedId;
            task.DueDate = updatedTask.DueDate;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/tk/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task is null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/tk/expired
        [HttpGet("expired")]
        public async Task<ActionResult<List<NomTask>>> GetExpiredTasks()
        {
            var today = DateTime.Today;
            var expiredTasks = await _context.Tasks
                .Where(t => t.DueDate < today)
                .ToListAsync();
            return expiredTasks;
        }

        // GET: api/tk/active
        [HttpGet("active")]
        public async Task<ActionResult<List<NomTask>>> GetActiveTasks()
        {
            var today = DateTime.Today;
            var activeTasks = await _context.Tasks
                .Where(t => t.DueDate >= today)
                .ToListAsync();
            return activeTasks;
        }

        // GET: api/tk/date/{date}
        [HttpGet("date/{date}")]
        public async Task<ActionResult<List<NomTask>>> GetTasksByDate(DateTime date)
        {
            var tasksByDate = await _context.Tasks
                .Where(t => t.DueDate.Date == date.Date)
                .ToListAsync();
            return tasksByDate;
        }

        // GET: api/tk/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<NomTask>>> GetTasksByUser(int userId)
        {
            var tasksByUser = await _context.Tasks
                .Where(t => t.AssignedId == userId.ToString())
                .ToListAsync();
            return tasksByUser;
        }
    }
}
