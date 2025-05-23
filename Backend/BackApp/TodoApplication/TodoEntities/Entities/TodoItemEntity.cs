using TodoList.BackApp.TodoApplication.TodoEntities.Enums;

namespace TodoList.BackApp.TodoApplication.TodoEntities.Entities
{
    public class TodoItemEntity
    {

        public int Id { get; set; } // Primary Key
        public string? Title { get; set; } // Görev başlığı
        public string? Description { get; set; } // Açıklama (opsiyonel)
        public bool IsCompleted { get; set; } = false; // Tamamlandı mı?
        public DateTimeOffset CreatedAt { get; set; }

       
        public UserEntity User { get; set; }
        public PriorityLevel Priority { get; set; }
    }
}
