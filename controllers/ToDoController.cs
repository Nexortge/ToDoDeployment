using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.context;
using Todo.entities;

namespace todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly TodoContext _context;

        public ToDoController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/todo
        [HttpGet]
        public IActionResult Get()
        {
            var todos = _context.ToDos.ToList();
            return Ok(todos);
        }

        // GET: api/todo/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        // POST: api/todo
        [HttpPost]
        public IActionResult Post([FromBody] ToDo todo)
        {
            _context.ToDos.Add(todo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        // PUT: api/todo/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ToDo todo)
        {
            if (id != todo.Id) return BadRequest();

            var existing = _context.ToDos.Find(id);
            if (existing == null) return NotFound();

            existing.Title = todo.Title;
            existing.IsCompleted = todo.IsCompleted;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/todo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.ToDos.Find(id);
            if (todo == null) return NotFound();

            _context.ToDos.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}