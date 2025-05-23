using MediatR;
using TodoList.BackApp.TodoApplication.TodoEntities;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities; // TodoItemEntity ve ApplicationDbContext burada tanımlı

namespace TodoList.BackApp.TodoApplication.Application.CommandHandlers
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItemEntity>
    {
        private readonly BackApp.TodoEntities.Entities.Entities.ApplicationDbContext _context;

        public CreateTodoItemCommandHandler(BackApp.TodoEntities.Entities.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItemEntity> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            TodoItemEntity item = new TodoItemEntity
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                CreatedAt = DateTimeOffset.Now
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return item;
        }
    }

}
