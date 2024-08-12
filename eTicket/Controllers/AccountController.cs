using System.Data;
using eTicket.Data;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _repository;

        public AccountController()
        {
            _repository = new UserRepository();
        }

        [HttpPost]
        public IActionResult Login(Login loginModel)
        {
            var userTable = _repository.GetUsers();
            DataRow foundUser = null;
            foreach (DataRow user in userTable.Rows)
            {
                if (user["Email"].ToString() == loginModel.Email && user["Password"].ToString() == loginModel.Password)
                {
                    foundUser = user;
                    break;
                }
            }

            if (foundUser == null)
            {
                // Redirect to a different page with an error message
                TempData["ErrorMessage"] = "Invalid email or password.";
                return RedirectToAction("AccessDenied");
            }

            if (loginModel.RememberMe)
            {
                var cookieOption = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("Email", loginModel.Email, cookieOption);
                Response.Cookies.Append("Password", loginModel.Password, cookieOption);
            }

            // Redirect to a different page indicating successful login
            return RedirectToAction("LoginComplete");
        }

        // GET
        public IActionResult Login()
        {
            return View(new Login());
        }

        public IActionResult RegisterCompleted()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult LoginComplete()
        {
            return View();
        }

        public IActionResult LoginFailure()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (register.Password != register.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View("Register", register);
            }

            var userTable = _repository.GetUsers();
            DataRow foundUser = null;
            foreach (DataRow user in userTable.Rows)
            {
                if (user["Email"].ToString() == register.EmailAddress)
                {
                    foundUser = user;
                    break;
                }
            }
            if (foundUser != null)
            {
                ModelState.AddModelError("", "User with this email already exists. Instead Opt for Login.");
                return View("Register", register);
            }
            if (register.RememberMe)
            {
                var cookieOption = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7)
                };
                Response.Cookies.Append("Email", register.EmailAddress, cookieOption);
                Response.Cookies.Append("Password", register.Password, cookieOption);
            }
            _repository.AddUser(register);
            return RedirectToAction("RegisterCompleted");
        }

        public IActionResult Register()
        {
            return View(new Register());
        }
    }
}
