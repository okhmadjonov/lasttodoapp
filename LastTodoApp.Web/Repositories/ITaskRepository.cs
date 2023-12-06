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
        Task Add(Domain.Dto.TaskDto task, string userId, string username, string email);
        Task Update(int id, Domain.Dto.TaskDto  taskViewModel, string userId, string username, string email);
        Task Delete(int id, string userId, string username);

    }
}
