using LastTodoApp.Domain.Entities;

using LastTodoApp.Domain.Entities.TaskViewModel;
using LastTodoApp.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LastTodoApp.Web.Controllers
{
  
    public class TaskController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITaskRepository _taskRepository;


        public TaskController(UserManager<User> userManager, ITaskRepository taskRepository)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
        }



        // Get All Task
        public async Task<ViewResult> Index()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return View("Index", tasks);
        }

        [HttpGet]
        public async Task<ViewResult> Add() {

            return View();
        
        }

        //Add New Task
        [HttpPost]
        public async Task<IActionResult> Add(TaskViewModel taskViewModel) 
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Add(taskViewModel, userId!, username!);
            var tasks =  await _taskRepository.GetAllTasks();
            return View("Index", tasks);

        }

        // Update task
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetSingleTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost]
         public async Task<IActionResult> Edit(int id,TaskViewModel taskViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Update(id, taskViewModel, userId!, username!);
            var tasks = await _taskRepository.GetAllTasks();
            return View("Index", tasks);

        }


        // Delete task
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskRepository.GetSingleTask(id);

            if (task == null)
            {
                return NotFound(); 
            }

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.GetUserAsync(User);
            var username = user!.UserName;
            await _taskRepository.Delete(id, userId!, username!);
            var tasks = await _taskRepository.GetAllTasks();
            return View("Index", tasks);
        }
    }
}
