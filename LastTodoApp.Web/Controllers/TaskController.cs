using LastTodoApp.Domain.Entities;

using LastTodoApp.Domain.Entities.TaskViewModel;
using LastTodoApp.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
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
        //public async Task<ViewResult> Index(string searchEmail)
        //{

        //    var tasks = await _taskRepository.GetAllTasks();
        //    if (!string.IsNullOrEmpty(searchEmail))
        //    {

        //        tasks = tasks.Where(t => t.User?.Email == searchEmail).ToList();
        //    }

        //    return View("Index", tasks);

        //}

        public async Task<IActionResult> Index(string searchEmail, int? page)
        {
            int pageSize = 5; // Number of tasks per page
            var tasks = await _taskRepository.GetAllTasks();

            if (!string.IsNullOrEmpty(searchEmail))
            {
                tasks = tasks.Where(t => t.User?.Email == searchEmail).ToList();
            }

            var pageNumber = page ?? 1;
            var paginatedTasks = tasks.ToPagedList(pageNumber, pageSize);

            return View(paginatedTasks);
        }





        [HttpGet]
        public async Task<ViewResult> Add() {

            return View();
        
        }

        //Add New Task
        [HttpPost]
        
        public async Task<IActionResult> Add(Domain.Entities.Task task, string email) 
        {
            var user = await _userManager.FindByEmailAsync(email!);

            if (user == null)
            {
                TempData["Error"] = "User with the specified email does not exist.";
                return View();
            }

            var userId = _userManager.GetUserId(User);
            var username = user.UserName;

            await _taskRepository.Add(task, userId!, username!, email);

            return RedirectToAction("Index");

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
           
            return RedirectToAction("Index");

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
           
            return RedirectToAction("Index");
        }
    }
}
