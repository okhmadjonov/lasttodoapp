﻿using LastTodoApp.Domain.Entities;
using LastTodoApp.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTodoApp.Domain.Dto
{
    public class TaskDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }

        public string UserEmail { get; set; }

    }
}
