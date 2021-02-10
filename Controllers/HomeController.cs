using LibraryProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LibraryProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        { 
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            #region Register admin and librarian if there are no users in DB 
            if (!_userManager.Users.Any())
            {
                const string adminsPassword = "/LNZX(94jd";
                const string librariansPassword = "/LNZX(94jd";
                var admin = new ApplicationUser
                {
                    Email = "admin3@gmail.com",
                    UserName = "Admin3"
                };
                var librarian = new ApplicationUser
                {
                    Email = "librarian3@gmail.com",
                    UserName = "Librarian3"
                };
                await _userManager.CreateAsync(admin, adminsPassword);
                await _userManager.AddToRoleAsync(admin, "admin");

                await _userManager.CreateAsync(librarian, librariansPassword);
                await _userManager.AddToRoleAsync(librarian, "librarian");
            }
            #endregion
            if (await _roleManager.RoleExistsAsync("admin")
                || await _roleManager.RoleExistsAsync("librarian")
                || await _roleManager.RoleExistsAsync("customer"))
            {
                return View();
            }
            await _roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "librarian" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "customer" });
            return RedirectToAction("Privacy");
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateCustomerViewModel viewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var user = new ApplicationUser()
            {
                Email = viewModel.Email,
                UserName = viewModel.Email
            };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded) return RedirectToAction("Index");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
