using LastTodoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastTodoApp.Domain.Dto
{
    public class RoleTaskDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public List<Entities.Task> Tasks { get; set; }
    }
}
