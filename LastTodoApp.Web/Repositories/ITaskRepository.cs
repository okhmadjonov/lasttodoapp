using LastTodoApp.Domain.Entities;
using LastTodoApp.Domain.Entities.TaskViewModel;
using Microsoft.AspNetCore.Mvc;

using Task = System.Threading.Tasks.Task;

namespace LastTodoApp.Web.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Domain.Entities.Task>> GetAllTasks();
        Task<Domain.Entities.Task> GetSingleTask(int id);
        Task Add(TaskViewModel taskViewModel, string userId, string username);
        Task Update(int id, TaskViewModel taskViewModel, string userId, string username);
        Task Delete(int id, string userId, string username);

    }
}
