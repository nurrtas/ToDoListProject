using MediatR;
namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.CommandHandlers
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, TodoItem>

    {
        private readonly Entities1.ApplicationDbContext _context;

        public CreateTodoItemCommandHandler(Entities1.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TodoItem> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var item = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted,
                UserId = request.UserId,
                CreatedAt = DateTimeOffset.Now
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
