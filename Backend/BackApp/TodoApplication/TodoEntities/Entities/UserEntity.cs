using Microsoft.AspNetCore.Identity;

namespace TodoList.BackApp.TodoApplication.TodoEntities.Entities
{
    public class UserEntity : IdentityUser
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string? Phone { get; set; }
        public class ApplicationUser : IdentityUser
        {
            public string? Message { get; set; }  // nullable yapıldı
        }

        public DateTime CreatedAt { get; set; }
    }
}
