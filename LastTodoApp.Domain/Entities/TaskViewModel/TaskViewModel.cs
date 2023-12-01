﻿using LastTodoApp.Domain.Entities.enums;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace LastTodoApp.Domain.Entities.TaskViewModel
{
    public class TaskViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "DueDate is required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public Status Status { get; set; }
    }
}
