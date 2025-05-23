using TodoList.BackApp.TodoApplication.TodoEntities.Entities;

namespace TodoList.BackApp.TodoApplication.TodoEntities.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItemEntity> CreateAsync(TodoItemEntity item);
        Task DeleteAsync(int id);
        Task<IEnumerable<TodoItemEntity>> GetAllAsync();
        Task<TodoItemEntity?> GetAsync(int id);
        Task<TodoItemEntity> UpdateAsync(TodoItemEntity item);
    }
}
