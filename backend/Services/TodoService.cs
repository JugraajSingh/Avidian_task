using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task<TodoItem> AddAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);
    }

    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}