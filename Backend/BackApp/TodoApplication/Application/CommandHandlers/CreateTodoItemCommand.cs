using MediatR;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;

namespace TodoList.BackApp.TodoApplication.Application.CommandHandlers


{
    public class CreateTodoItemCommand : IRequest<TodoItemEntity>
    {
        public string Title { get; set; } 
        public string? Description { get; set; }
        public bool IsCompleted { get; set; } = false;

        public CreateTodoItemCommand()
        {
        }

        public CreateTodoItemCommand(string title, string? description = null, bool isCompleted = false)
        {
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
        }
    }
}
