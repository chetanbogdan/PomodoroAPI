using PomodoroAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PomodoroAPI.Repository
{
    public class TodoRepository : ITodoRepository
    {
        public List<TodoItem> todoItems;

        public TodoRepository()
        {
            if(todoItems == null)
            {
                todoItems = new List<TodoItem>();
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            todoItems.Remove(todoItems.First(x => x.Id == id));
        }

        public async Task<IEnumerable<TodoItem>> GetAllItemsAsync()
        {
            return todoItems;
        }

        public async Task<TodoItem> GetTodoAsync(Guid id)
        {
            return todoItems.FirstOrDefault(x => x.Id == id);
        }

        public async Task<TodoItem> SaveTodoAsync(TodoItem item)
        {
            item.Id = Guid.NewGuid();
            todoItems.Add(item);
            return item;
        }

        public async Task UpdateTodoAsync(Guid id, TodoItem item)
        {
            todoItems.Remove(todoItems.First(x => x.Id == id));
            todoItems.Add(item);
        }
    }
}
