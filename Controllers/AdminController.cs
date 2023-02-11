﻿using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Account> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<Account> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RemoveUserByUserName(string name)
        {
            Account user = await _userManager.FindByNameAsync(name);
            _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveUserById(string id)
        {
            Account user = await _userManager.FindByIdAsync(id);
            _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        // Finn lösning på en slags 'squelch'/'mute' för en period av tid.
        //public async Task<IActionResult> RestrictUserCalled(string name)
        //{

        //}


    }
}