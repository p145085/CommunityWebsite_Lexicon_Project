using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("Admin")]
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

        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(Guid id)
        {
            string convId = id.ToString();
            var role = await _roleManager.FindByIdAsync(convId);
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

            return View(rolesViewModel);
        }

        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> AddToRole(Guid userId, string roleId)
        {
            string convId = userId.ToString();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(convId);
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

        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> RemoveFromRole(Guid userId, string roleId)
        {
            string convId = userId.ToString();
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(convId);
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
