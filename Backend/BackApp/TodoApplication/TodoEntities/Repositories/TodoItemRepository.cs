using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;
using TodoList.BackApp.TodoEntities.Entities.Entities;
using ApplicationDbContext = TodoList.BackApp.TodoEntities.Entities.Entities.ApplicationDbContext;

namespace TodoList.BackApp.TodoApplication.TodoEntities.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Yeni TodoItemEntity ekleme
        public async Task<TodoItemEntity> CreateAsync(TodoItemEntity item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // TodoItemEntity silme
        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        // Tüm TodoItemEntity'ları getirme
        public async Task<IEnumerable<TodoItemEntity>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // TodoItemEntity'ı ID ile getirme
        public async Task<TodoItemEntity?> GetAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        // TodoItemEntity güncelleme
        public async Task<TodoItemEntity> UpdateAsync(TodoItemEntity item)
        {
            var existingItem = await _context.TodoItems.FindAsync(item.Id);
            if (existingItem != null)
            {
                existingItem.Title = item.Title;
                existingItem.IsCompleted = item.IsCompleted;
                existingItem.Priority = item.Priority;
                await _context.SaveChangesAsync();
            }
            return existingItem!;
        }

    }
}
