using PomodoroAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PomodoroAPI.Repository
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllItemsAsync();

        Task<TodoItem> GetTodoAsync(Guid id);

        Task<TodoItem> SaveTodoAsync(TodoItem item);

        Task UpdateTodoAsync(Guid id, TodoItem item);

        Task DeleteTodoAsync(Guid id);
    }
}
