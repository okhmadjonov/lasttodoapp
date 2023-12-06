using LastTodoApp.DataContext.Data;
using LastTodoApp.Domain.Entities;

namespace LastTodoApp.Web.Repositories.Services
{
    public class UserService
    {

        private readonly AppDbContext  _context;

        // Constructor injection for your DbContext
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Get all users method
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

    }
}
