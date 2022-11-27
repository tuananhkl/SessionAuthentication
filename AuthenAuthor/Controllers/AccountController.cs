using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenAuthor.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenAuthor.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthManager _authManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAuthManager authManager, IHttpContextAccessor httpContextAccessor)
        {
            _authManager = authManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
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
                    
                    if (httpContext != null)
                    {
                        httpContext.Session.SetString("username", username);
                        httpContext.Session.SetString("password", password);
                        
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