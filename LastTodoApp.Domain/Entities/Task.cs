using LastTodoApp.Domain.Entities.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTodoApp.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
              
        public string UserEmail { get; set; }
        public User? User { get; set; }

    }
}
