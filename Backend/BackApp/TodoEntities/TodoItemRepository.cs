using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoEntities.Entities;
using TodoList.BackApp.TodoEntities.Entities.Entities1;
using TodoList.BackApp.TodoEntities.Entities.EntityCobnfigurations;
using TodoList.BackApp.TodoEntities.Repositories;

namespace TodoList.Backend.TodoEntities
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly BackApp.TodoEntities.Entities.Entities1.ApplicationDbContext _context;

        public TodoItemRepository(BackApp.TodoEntities.Entities.Entities1.ApplicationDbContext context)
        {
            _context = context;
        }

        // Yeni TodoItem ekleme
        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // TodoItem silme
        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        // Tüm TodoItem'ları getirme
        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // TodoItem'ı ID ile getirme
        public async Task<TodoItem?> GetAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        // TodoItem güncelleme
        public async Task<TodoItem> UpdateAsync(TodoItem item)
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
