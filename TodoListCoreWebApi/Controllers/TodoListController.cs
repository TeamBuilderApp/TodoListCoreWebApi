using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TodoListCoreWebApi.Models;

namespace TodoListCoreWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {

        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ILogger<TodoListController> logger)
        {
            _logger = logger;
        }

        //Get all todo list items
        //Returns a response body as an array of strings.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListItemModelDto>>> Get()
        {
            /*
                         return await _context.TodoList
                 .Select(x => TodoList.Models.TodoList.TodoListItemModelDto(x))
                 .ToListAsync();
             */
            return NoContent();
        }

        //Gets a single todo list item by item by its id number.
        //Returns a response body as a string.
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListItemModelDto>> Get(long id)
        {
            /*TodoList.Models.TodoList? todoListItem = await _context.TodoList.FindAsync(id);

            if (todoListItem == null)
            {
                return NotFound(); //404
            }

            return TodoList.Models.TodoList.TodoListItemModelDto(todoListItem);*/
            return NoContent();
        }

        //Updates a single todo list item by item its id number.
        //Returns a response body as a string.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, TodoListItemModelDto todoListItemModelDto)
        {
            /*todoListItemModelDto.Id = id;

            var todoListItem = await _context.todoList.FindAsync(id);
            if (todoListItem == null)
            {
                return NotFound(); //404
            }

            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!todoListItemExists(id))
            {
                return NotFound(); //404
            }
            */
            return NoContent();
        }

        //Saves a new todo list item with max ID + 1, to an Azure SQL Relational DB.
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoListItemModelDto>> Post(TodoListItemModelDto todoListItemDto)
        {
            Models.TodoListItemModel todoListItem = new(todoListItemDto);

            try
            {
                _ = _context.TodoListItemModel.Add(todoListItem);
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound(); //404
            }

            return CreatedAtAction(
                nameof(Get),
                new { id = todoListItem.Id },
                todoListItemDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            /*TodoList.Models.TodoList? todoListItem = await _context.TodoList.FindAsync(id);
            if (todoListItem == null)
            {
                return NotFound(); //404
            }

            try
            {
                _ = _context.TodoList.Remove(todoListItem);
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoListItemExists(id))
            {
                return NotFound(); //404
            }
            */
            return NoContent();
        }

        // Returns true if a todo item could be found by ID.
        private bool TodoListItemExists(long id)
        {
            //return _context.todoList.Any(e => e.Id == id);
            return false;
        }

    }
}
