using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;
using TodoList.BackApp.TodoApplication.TodoEntities; // ApplicationDbContext için ekleyin

namespace TodoList.BackApp.TodoApplication.Application.QueryHandlers
{
    public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItemEntity?>
    {
        private readonly BackApp.TodoEntities.Entities.Entities.ApplicationDbContext _context; 

        public GetTodoItemByIdQueryHandler(BackApp.TodoEntities.Entities.Entities.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItemEntity?> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.TodoItems.Include(x => x.User)
                                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
    public class GetTodoItemByIdQuery : IRequest<TodoItemEntity?>
    {
        public int Id { get; }

        public GetTodoItemByIdQuery(int id)
        {
            Id = id;
        }
    }

}