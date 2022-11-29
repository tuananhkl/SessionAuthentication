using AuthenAuthor.DbContext;
using AuthenAuthor.Models;
using AuthenAuthor.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenAuthor.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null && httpContext.Session.GetString("username") is null)
            {
                //ViewData["authError"] = "You didn't have permission. Please login";   
                //TempData["authError"] = "You didn't have permission. Please <a asp-area=\"\" asp-controller=\"Account\" asp-action=\"Index\">Login</a>";
                TempData["authError"] = "You didn't have permission. Please Login";
                
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            
            var users = await _userRepository.GetAll();
            if (users is null)
            {
                return Problem("Can't find any users");
            }
            
            return View(users.ToList());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user is null)
            {
                return NotFound($"User with id {id} is not found");
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,DateOfBirth,Address,Email,Age,Gender")] User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,DateOfBirth,Address,Email,Age,Gender")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userRepository.Update(user);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest("Id must be >= 0");
            }

            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound($"User with id {id} is not found");
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // if (_context.Users == null)
            // {
            //     return Problem("Entity set 'AppDbContext.Users'  is null.");
            // }

            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                await _userRepository.Delete(user);
            }
            
            return RedirectToAction(nameof(Index));
        }

        // private IActionResult AuthorizeBySession()
        // {
        //     var httpContext = _httpContextAccessor.HttpContext;
        //     if(httpContext != null && httpContext.Session.GetString("username") is not null)
        //     {
        //         return RedirectToRoute(new { controller = "Home", action = "Index" });
        //     }
        // }
    }
}