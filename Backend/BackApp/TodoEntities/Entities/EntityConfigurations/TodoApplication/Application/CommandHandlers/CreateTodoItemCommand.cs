using MediatR;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.CommandHandlers
{
    public class CreateTodoItemCommand : IRequest<TodoItem>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
    }
}
