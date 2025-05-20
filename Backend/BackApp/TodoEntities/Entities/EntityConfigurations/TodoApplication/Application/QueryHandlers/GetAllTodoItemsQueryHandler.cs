using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoEntities.Entities.Entities1;
using TodoList.BackApp.TodoEntities.Entities.EntityCobnfigurations;

namespace TodoList.BackApp.TodoEntities.Entities.EntityConfigurations.TodoApplication.Application.QueryHandlers
{
    public class GetAllTodoItemsQueryHandler
    {
        private readonly Entities1.ApplicationDbContext _context;

        public GetAllTodoItemsQueryHandler(Entities1.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> Handle(GetAllTodoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.Include(x => x.User).ToListAsync(cancellationToken);
        }
    }
}
