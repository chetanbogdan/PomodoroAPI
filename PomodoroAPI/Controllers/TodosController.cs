using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PomodoroAPI.Models;
using PomodoroAPI.Repository;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PomodoroAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await todoRepository.GetAllItemsAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            var todo = await todoRepository.GetTodoAsync(id);
            return Ok(todo);
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTodo(TodoItem item)
        {
            var todo = await todoRepository.SaveTodoAsync(item);
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodo(Guid id, TodoItem item)
        {
            var todo = await todoRepository.GetTodoAsync(id);

            if(todo == null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.Description = item.Description;

            await todoRepository.UpdateTodoAsync(todo.Id, todo);

            return NoContent();
        }

        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            await todoRepository.DeleteTodoAsync(id);

            return Ok();
        }
    }
}
