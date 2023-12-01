using LastTodoApp.DataContext.Data;
using LastTodoApp.Domain.Entities;
using LastTodoApp.Domain.Entities.RegisterViewModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastTodoApp.DataContext.Services
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // Handle Model State Message Error
        public string HandleModelStateErrors(ModelStateDictionary keyValuePairs, string defaultErrorMessage="Validation failed") {

            if (keyValuePairs.IsValid) return "success";

            var errorMessages = keyValuePairs.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return errorMessages.Count > 0 ? errorMessages[0] : defaultErrorMessage;
        }


        // Checking User Email and Password
        public async Task<(bool isAuthenticated, User user)> CheckUserAsync(string email, string password) { 
        
        
            var user = await _userManager.FindByEmailAsync(email); 
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                return (true, user)!;

            }
            return (false, user)!;
        
        }


        // Register User

        public async Task<bool> RegisterUser(RegisterViewModel registerViewModel) 
        { 
            var newUser = new User() { Email = registerViewModel.Email, UserName = registerViewModel.Username };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            await _userManager.AddToRoleAsync(newUser, ERole.USER.ToString());
            return true;

        }


        // Add User
        public async Task<bool> AddUser(RegisterViewModel model, string role)
        {
            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
               
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, role);
            return true;
        }

        // Check user
        public async Task<bool> CheckUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                return true;
            }
            return false;
        }

    }
}
