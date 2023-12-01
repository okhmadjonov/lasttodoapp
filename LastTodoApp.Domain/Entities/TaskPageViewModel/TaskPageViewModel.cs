using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTodoApp.Domain.Entities.TaskPageViewModel
{
    public class TaskPageViewModel
    {
        public List<LastTodoApp.Domain.Entities.Task> Tasks { get; set; }
        public List<LastTodoApp.Domain.Entities.User> Users { get; set; }
    }
}
