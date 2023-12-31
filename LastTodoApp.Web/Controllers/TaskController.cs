﻿using LastTodoApp.Domain.Dto;
using LastTodoApp.Domain.Entities;

using LastTodoApp.Domain.Entities.TaskViewModel;
using LastTodoApp.Web.Repositories;
using LastTodoApp.Web.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using X.PagedList;
namespace LastTodoApp.Web.Controllers
{


    [Authorize(Roles ="ADMIN, USER, MANAGER")]
    public class TaskController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ITaskRepository _taskRepository;
      

        public TaskController(UserManager<User> userManager, ITaskRepository taskRepository)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
           
        }
        public async Task<IActionResult> Index(string searchEmail, int? page)
        {
            int pageSize = 10;
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
        [Authorize(Roles = "ADMIN, MANAGER")]
        public async Task<ViewResult> Add()
        {
            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "Email", "Email");

            var taskDto = new Domain.Dto.TaskDto()
            {
                DueDate = DateTime.Now
            };

            return View(taskDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN, MANAGER")]
        public async Task<IActionResult> Add(Domain.Dto.TaskDto taskDto)
        {
            var user = await _userManager.FindByEmailAsync(taskDto.UserEmail);

            if (user == null)
            {
                return NotFound($"User with email '{taskDto.UserEmail}' not found.");
            }

            var userId = user.Id;
            var username = user.UserName;

            await _taskRepository.Add(taskDto, userId, username, taskDto.UserEmail);

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskRepository.GetSingleTask(id);

            if (task == null)
            {
                return NotFound();
            }

            var users = await _userManager.Users.ToListAsync();
            ViewBag.Users = new SelectList(users, "Email", "Email");

            var taskDto = new Domain.Dto.TaskDto
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status,
                UserEmail = task.UserEmail,
            };

            return View(taskDto);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int id, Domain.Dto.TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                var users = await _userManager.Users.ToListAsync();
                ViewBag.Users = new SelectList(users, "Email", "Email");

                return View(taskDto);
            }

            try
            {
                var task = await _taskRepository.GetSingleTask(id);

                if (task == null)
                {
                    return NotFound();
                }

                var user = await _userManager.FindByEmailAsync(task.UserEmail);

                if (user == null)
                {
                    TempData["Error"] = "User with the specified email does not exist.";
                    return View(task);
                }

                await _taskRepository.Update(id, taskDto, user.Id, user.UserName, taskDto.UserEmail);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View(taskDto);
            }
        }




        // Delete task
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
