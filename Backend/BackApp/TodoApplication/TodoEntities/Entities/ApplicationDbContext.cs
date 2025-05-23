using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoList.BackApp.TodoApplication.TodoEntities.Entities;

namespace TodoList.BackApp.TodoEntities.Entities.Entities
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItemEntity> TodoItems { get; set; }
        
    }
}
