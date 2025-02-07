using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoService service, ILogger<TodoController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves all todo items
        /// </summary>
        /// <returns>A list of todo items</returns>
        /// <response code="200">Returns the list of todo items</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TodoItem>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            try
            {
                _logger.LogInformation("Retrieving all todo items");
                var items = await _service.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all todo items");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Retrieves a specific todo item by id
        /// </summary>
        /// <param name="id">The ID of the todo item</param>
        /// <returns>The requested todo item</returns>
        /// <response code="200">Returns the requested todo item</response>
        /// <response code="404">If the item wasn't found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItem), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            try
            {
                _logger.LogInformation("Retrieving todo item with ID: {Id}", id);
                var item = await _service.GetByIdAsync(id);
                
                if (item == null)
                {
                    _logger.LogWarning("Todo item with ID: {Id} not found", id);
                    return NotFound($"Todo item with ID {id} not found");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Creates a new todo item
        /// </summary>
        /// <param name="item">The todo item to create</param>
        /// <returns>The created todo item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or invalid</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Todo item cannot be null");
                }

                _logger.LogInformation("Creating new todo item");
                var createdItem = await _service.AddAsync(item);
                
                _logger.LogInformation("Created todo item with ID: {Id}", createdItem.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating todo item");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Updates an existing todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to update</param>
        /// <param name="item">The updated todo item</param>
        /// <returns>The updated todo item</returns>
        /// <response code="200">Returns the updated item</response>
        /// <response code="400">If the IDs don't match or the item is invalid</response>
        /// <response code="404">If the item wasn't found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TodoItem), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> Update(int id, [FromBody] TodoItem item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Todo item cannot be null");
                }

                if (id != item.Id)
                {
                    _logger.LogWarning("ID mismatch. Path ID: {PathId}, Item ID: {ItemId}", id, item.Id);
                    return BadRequest("ID mismatch between path and item");
                }

                _logger.LogInformation("Updating todo item with ID: {Id}", id);
                var updatedItem = await _service.UpdateAsync(id, item);

                if (updatedItem == null)
                {
                    _logger.LogWarning("Todo item with ID: {Id} not found for update", id);
                    return NotFound($"Todo item with ID {id} not found");
                }

                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        /// <summary>
        /// Deletes a specific todo item
        /// </summary>
        /// <param name="id">The ID of the todo item to delete</param>
        /// <returns>No content</returns>
        /// <response code="204">If the item was successfully deleted</response>
        /// <response code="404">If the item wasn't found</response>
        /// <response code="500">If there was an internal server error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("Deleting todo item with ID: {Id}", id);
                var item = await _service.GetByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning("Todo item with ID: {Id} not found for deletion", id);
                    return NotFound($"Todo item with ID {id} not found");
                }

                await _service.DeleteAsync(id);
                _logger.LogInformation("Successfully deleted todo item with ID: {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting todo item with ID: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}