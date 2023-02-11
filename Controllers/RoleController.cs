using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Account> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<Account> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoleCalled(string name)
        {
            IdentityRole role = new IdentityRole(name);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            String message = ("Errors: ");

            //StringBuilder stringBuilder = new StringBuilder("Errors");
            foreach (var item in result.Errors)
            {
                //stringBuilder.Append(item.Description + " | ");
                message += item.Description + (" ");
            }
            ViewBag.Msg = message;
            //ViewBag.Msg = stringBuilder.ToString();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string id, string msg = null)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            ManageRolesViewModel rolesViewModel = new ManageRolesViewModel();

            rolesViewModel.Role = role;
            rolesViewModel.AccountsWithRole = await _userManager.GetUsersInRoleAsync(role.Name);
            rolesViewModel.AccountsWithNoRole = _userManager.Users.ToList();

            foreach (var item in rolesViewModel.AccountsWithRole)
            {
                rolesViewModel.AccountsWithNoRole.Remove(item);
            }

            ViewBag.Msg = msg;
            return View(rolesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddToRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageUserRoles), new { msg = " User have been successfully added to the role", id = role.Id });
            }
            return RedirectToAction(nameof(ManageUserRoles), new { msg = " Add role to user failed", id = role.Id });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(string userId, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ManageUserRoles), new { msg = " User have been successfully removed from the role", id = role.Id });
            }
            return RedirectToAction(nameof(ManageUserRoles), new { msg = " Remove the role from user, failed", id = role.Id });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
