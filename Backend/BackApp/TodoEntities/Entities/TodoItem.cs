namespace TodoList.BackApp.TodoEntities.Entities
{
    public class TodoItem
    {
        public int Id { get; set; } // Primary Key
        public string? Title { get; set; } // Görev başlığı
        public string? Description { get; set; } // Açıklama (opsiyonel)
        public bool IsCompleted { get; set; } = false; // Tamamlandı mı?
        public PriorityLevel Priority { get; set; } = PriorityLevel.Medium; // Varsayılan öncelik: Medium
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now; // Varsayılan olarak şu anki tarih

        // ✅ Kullanıcı ile ilişki
        public string UserId { get; set; } // Foreign key
        public AppUser User { get; set; } // Navigation property, ilişkiyi yönetir
    }
}
