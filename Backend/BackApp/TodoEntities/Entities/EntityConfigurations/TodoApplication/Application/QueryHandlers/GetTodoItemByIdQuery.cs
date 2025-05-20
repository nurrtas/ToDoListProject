using MediatR;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.QueryHandlers
{
    public class GetTodoItemByIdQuery : IRequest<TodoItem?>
    {
        public int Id { get; }

        public GetTodoItemByIdQuery(int id)
        {
            Id = id;
        }
    }
}
