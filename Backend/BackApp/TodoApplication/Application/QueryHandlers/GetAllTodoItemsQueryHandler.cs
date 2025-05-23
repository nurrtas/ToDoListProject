
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;
using TodoList.BackApp.TodoApplication.TodoEntities.ItemDtos;
using TodoList.BackApp.TodoEntities.Entities.Entities;

namespace TodoList.BackApp.TodoApplication.Application.QueryHandlers
{
    public class GetAllTodoItemsQueryHandler : IRequestHandler<GetAllTodoItemsRequest, List<TodoItemEntity>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllTodoItemsQueryHandler()
        {
        }

        public GetAllTodoItemsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemEntity>> Handle(GetAllTodoItemsRequest request, CancellationToken cancellationToken)
        {
            var query = _context.TodoItems.Include(x => x.User).AsQueryable();
                 return await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
