using LastTodoApp.DataContext.Services;
using LastTodoApp.Domain.Entities;
using LastTodoApp.Domain.Entities.LoginViewModel;
using LastTodoApp.Domain.Entities.RegisterViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LastTodoApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly SignInManager<User> _signInManager;


        public AccountController(AccountService accountService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            TempData.Clear();
            return View();
        }

        public IActionResult Register()
        {
            TempData.Clear();
            return View("Register");
        }


        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginViewModel)
        {
            var validationError =  _accountService.HandleModelStateErrors(ModelState, "Please enter valid data");

            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                TempData.Keep("Error");
                return View();
            }

            var (isAuthenticated, user) = await _accountService.CheckUserAsync(loginViewModel.Email, loginViewModel.Password);

            if (!isAuthenticated)
            {
                TempData["Error"] = "Email or password is incorrect";
                return View();
            }

            await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            return RedirectToAction("Index", "Home");

        
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                return View("Register");
            }

            var isAuthenticated = await _accountService.CheckUser(registerViewModel.Email);

            if (isAuthenticated)
            {
                TempData["Error"] = "User with this email already exists";
                return View("Register");
            }

            await _accountService.RegisterUser(registerViewModel);

            ViewBag.Success = "Registration successful! You will be redirected to the login page in 3 seconds.";

            return View("Register");
        }

        [HttpGet]
        public IActionResult AddUserIndex()
        {
            return View("AddUser");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterViewModel model, string role)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                return View("AddUser");
            }

            var isAuthenticated = await _accountService.CheckUser(model.Email);

            if (isAuthenticated)
            {
                TempData["Error"] = "User with this email already exists";
                return View("AddUser");
            }
            await _accountService.AddUser(model, role);

            ViewBag.Success = "Registration successful! You will be redirected to the task page in 3 seconds.";

            return View("AddUser");
        }


        [HttpGet]
        public async Task<RedirectToActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
