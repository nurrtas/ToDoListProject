using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoEntities.Entities.Entities1;
using TodoList.BackApp.TodoEntities.Entities.EntityCobnfigurations;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.QueryHandlers
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItem?>
    {
        private readonly Entities1.ApplicationDbContext _context;

        public GetTodoItemByIdQueryHandler(Entities1.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.Include(x => x.User)
                                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
