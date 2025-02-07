// filepath: /Users/jux/Desktop/Avidian/backend/Models/TodoItem.cs
namespace backend.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }
    }
}