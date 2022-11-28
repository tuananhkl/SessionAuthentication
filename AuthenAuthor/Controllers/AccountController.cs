using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AuthenAuthor.Repository.Contract;
using AuthenAuthor.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenAuthor.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAuthManager authManager, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _authManager = authManager;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
        
        public IActionResult Details()
        {
            return View();
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (username is not null && password is not null)
            {
                var result = await _authManager.ValidateUser(username, password);
                if (result == true)
                {
                    var user = await _userRepository.GetByUserName(username);
                    if (user is null)
                    {
                        return NotFound("User is not found");
                    }
                    if (httpContext != null)
                    {
                        httpContext.Session.SetString("username", username);
                        httpContext.Session.SetString("password", password);
                        if (user.DateOfBirth != null)
                            httpContext.Session.SetString("dob", user.DateOfBirth.Value.Date.ToString(CultureInfo.InvariantCulture));
                        httpContext.Session.SetString("address", user.Address);
                        httpContext.Session.SetString("email", user.Email);
                        httpContext.Session.SetString("age", user.Age.ToString());
                        if (user.Gender == true)
                        {
                            httpContext.Session.SetString("gender", "Nam");
                        }
                        else
                        {
                            httpContext.Session.SetString("gender", "Nu");
                        }
                        
                    }
                    return View("Success");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.error = "User Name or Password can't be empty";
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}