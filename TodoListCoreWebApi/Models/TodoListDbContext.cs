using Microsoft.EntityFrameworkCore;

namespace TodoList.Models
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options) { }
        public DbSet<TodoList> TodoList { get; set; } = null!;
    }
}
