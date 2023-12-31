﻿using AutoMapper;
using LastTodoApp.DataContext.Data;
using LastTodoApp.Domain.Dto;
using LastTodoApp.Domain.Entities;
using LastTodoApp.Domain.Entities.TaskViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = LastTodoApp.Domain.Entities.Task;

namespace LastTodoApp.Web.Repositories.Services
{
    public class TaskService : ITaskRepository
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public TaskService(AppDbContext appDbContext, UserManager<User> userManager, IMapper mapper)
        {
            _context = appDbContext;
            _userManager = userManager;
            _mapper = mapper;   
        }
        public async System.Threading.Tasks.Task Add(TaskDto task, string userId, string username, string email)
        {
            var foundUser = await _context.Users.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Email == email);
            var tasknew = new Task
            {
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc),
                UserEmail= task.UserEmail,
            };
            if (foundUser.Tasks is null)
            {
                foundUser.Tasks = new List<Task> { tasknew };
            }
            else
            {
                foundUser.Tasks.Add(tasknew);
            }
            _context.Tasks.Add(tasknew);
            await _context.SaveChangesAsync(userId, username);
        }

        public async System.Threading.Tasks.Task Delete(int id, string userId, string username)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
            if (task is not null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync(userId, username);
            }
        }

        public async Task<Domain.Entities.Task> GetSingleTask(int id)
        {
            var task = await _context.Tasks.Include(u=> u.User).FirstOrDefaultAsync(x => x.Id == id);

        

            if (task is null)
            {
                throw new BadHttpRequestException("Task not found");
            }
            return task ?? throw new BadHttpRequestException("Task not found");
        }

        public async Task<List<Domain.Entities.Task>> GetAllTasks()
        {

           
            return await   _context.Tasks.Include(u=> u.User).OrderBy(t => t.Id).ToListAsync();
           // var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

          
        }


        public async System.Threading.Tasks.Task Update(int id, Domain.Dto.TaskDto taskDto, string userId, string username, string newEmail)
        {
            var task = await _context.Tasks.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                throw new BadHttpRequestException("Task not found");
            }

            // Update task properties
            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.Status = taskDto.Status;
            task.DueDate = DateTime.SpecifyKind(taskDto.DueDate, DateTimeKind.Utc);
            task.UserEmail = taskDto.UserEmail;

           
            await _context.SaveChangesAsync();
        }



    }
}
