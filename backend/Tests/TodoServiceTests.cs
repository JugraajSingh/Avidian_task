using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TodoServiceTests
{
    private readonly TodoService _service;
    private readonly TodoContext _context;

    public TodoServiceTests()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TodoTestDb")
            .Options;
        _context = new TodoContext(options);
        _service = new TodoService(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddItem()
    {
        var item = new TodoItem { Title = "Test", Description = "Test Description", Priority = "P1", CategoryId = "Work", Status = "todo" };
        var result = await _service.AddAsync(item);
        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllItems()
    {
        var items = await _service.GetAllAsync();
        Assert.NotNull(items);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnItem()
    {
        var item = new TodoItem { Title = "Test", Description = "Test Description", Priority = "P1", CategoryId = "Work", Status = "todo" };
        await _service.AddAsync(item);
        var result = await _service.GetByIdAsync(item.Id);
        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateItem()
    {
        var item = new TodoItem { Title = "Test", Description = "Test Description", Priority = "P1", CategoryId = "Work", Status = "todo" };
        await _service.AddAsync(item);
        item.Title = "Updated Test";
        var result = await _service.UpdateAsync(item.Id, item);
        Assert.NotNull(result);
        Assert.Equal("Updated Test", result.Title);
    }
}