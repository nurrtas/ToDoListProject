using MediatR;

namespace TodoList.Backend
{
    internal class TodoApplication
    {
        internal class Application
        {
            internal class QueryHandlers
            {
                internal class GetAllTodoItemsQuery : IRequest<object>
                {
                }
            }
        }
    }
}