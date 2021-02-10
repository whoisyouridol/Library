using LibraryProject.Models;
using LibraryProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateCustomerViewModel viewModel)
        {
            var user = new ApplicationUser
            {
                UserName = viewModel.Email,
                Email = viewModel.Email,
            };
            await _userManager.CreateAsync(user, viewModel.Password);
            await _userManager.AddToRoleAsync(user, "customer");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Manage()
        {
            var users = await _userManager.GetUsersInRoleAsync("customer");
            return View(users.ToList());
        }

        public async Task<ActionResult> Delete(string id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            IdentityResult result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded)
                return RedirectToAction("Manage");
            return Content("Error!");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(new EditCustomerViewModel
            {
                Id = user.Id,
                Email = user.Email,
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditCustomerViewModel viewModel)
        {
            var user = await _userManager.FindByIdAsync(viewModel.Id);

            var result = await _userManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);

            if (!result.Succeeded) return Content("Error");

            return RedirectToAction("Manage");
        }
    }
}
