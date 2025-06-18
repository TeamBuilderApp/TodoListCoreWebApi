using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    /* 
        Added a Data Transfer Object (DTO)!     
        "A DTO may be used to:
        Prevent over-posting.
        Hide properties that clients are not supposed to view.
        Omit some properties in order to reduce payload size.
        Flatten object graphs that contain nested objects. Flattened object graphs can be more convenient for clients.".
        Ref: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio

        Will map to a code-first approach using Entity Framework SQL,  using DBContext, and POCO's, eventually. For now let's get it working!
        A DbSet represents the collection of all entities in the context, or that can be queried from the database, of a given type. 
        DbSet objects are created from a DbContext.

        "SRP: The Single Responsibility Principle
        THERE SHOULD NEVER BE MORE THAN ONE REASON FOR A CLASS TO CHANGE.".
        Ref SRP: https://web.archive.org/web/20150202200348/http://www.objectmentor.com/resources/articles/srp.pdf
     */
    public class TodoListDto
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; } = "";

        [MaxLength(2500)]
        public string Description { get; set; } = "";
        public DateTime? DateTime { get; set; }

        public bool IsComplete { get; set; } = false;
    }

    public class TodoList : TodoListDto
    {
        //The secret field needs to be hidden from this app, but an administrative app could choose to expose it.
        private string? Secret { get; set; }
        public TodoList()
        {
        }

        public TodoList(TodoListDto todoListDto)
        {
            Id = todoListDto.Id;
            Description = todoListDto.Description;
            DateTime = todoListDto.DateTime;
            IsComplete = todoListDto.IsComplete;
        }
    }
}

