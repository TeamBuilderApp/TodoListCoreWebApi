using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoListDbContext _context;
        private readonly ILogger<TodoListController> _logger;

        public TodoListController(TodoListDbContext context, ILogger<TodoListController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TodoList
        // Get all todo list items.
        // Returns a response body as an array of strings.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListDto>>> GetTodoItems()
        {
            return await _context.TodoList
                .Select(x => TodoListItemToDto(x))
                .ToListAsync();
        }

        // GET: api/TodoList/id
        // Gets a single todo list item by item by its ID.
        // Returns a response body as a string.
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListDto>> Get(long id)
        {
            var todoItem = await _context.TodoList.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        // </snippet_GetByID>

        // PUT: api/TodoList/id
        // Updates a single todo list item by item its id number.
        // Returns a response body as a string.
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Update>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoListDto todoListDto)
        {
            if (id != todoListDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoList.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoListDto.Name;
            todoItem.IsComplete = todoListDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoListItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        // </snippet_Update>

        // POST: api/TodoList
        // Saves a new todo list item with max ID + 1, to an Azure SQL Relational DB.
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // <snippet_Create>
        [HttpPost]
        public async Task<ActionResult<TodoListDto>> Post(TodoListDto todoListDto)
        {
            var todoItem = new Models.TodoList
            {
                IsComplete = todoListDto.IsComplete,
                Name = todoListDto.Name,
                Description = todoListDto.Description,
                DateTime = todoListDto.DateTime
            };

            _context.TodoList.Add(todoItem);
            await _context.SaveChangesAsync();

            //Turn to DTO because we have to call the Get to fetch the latest.
            return CreatedAtAction(
                nameof(Get),
                new { id = todoItem.Id },
                TodoListItemToDto(todoItem));
        }
        // </snippet_Create>

        // DELETE: api/TodoList/id
        // Deletes a todo list item by ID from the list, if it exists.
        // <snippet_Delete>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoItem = await _context.TodoList.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoList.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // </snippet_Delete>

        // Returns true if a todo list item could be found by ID.
        private bool TodoListItemExists(long id)
        {
            return _context.TodoList.Any(e => e.Id == id);
        }

        //Converts a todo list item object to a todo list item object DTO.
        private static TodoListDto TodoListItemToDto(TodoList.Models.TodoList todoItem) =>
        new TodoListDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            IsComplete = todoItem.IsComplete
        };

    }
}
