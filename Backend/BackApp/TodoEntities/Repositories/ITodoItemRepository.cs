using TodoList.BackApp.TodoEntities.Entities;

namespace TodoList.BackApp.TodoEntities.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> CreateAsync(TodoItem item);
        Task DeleteAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetAsync(int id);
        Task<TodoItem> UpdateAsync(TodoItem item);
    }
}
