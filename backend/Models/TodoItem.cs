namespace backend.Models
{
    public class TodoItem
    {
        public int Id { get; set; } // Changed to int
        public string Title { get; set; } = string.Empty; // Initialized with default value
        public string Description { get; set; } = string.Empty; // Initialized with default value
        public string Status { get; set; } = "todo"; // Initialized with default value
        public string Priority { get; set; } = "low"; // Initialized with default value
        public string CategoryId { get; set; } = string.Empty; // Initialized with default value
        public DateTime DueDate { get; set; } = DateTime.UtcNow; // Initialized with default value
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Initialized with default value
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Initialized with default value
    }
}