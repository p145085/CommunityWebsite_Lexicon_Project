using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private ApplicationDbContext _context;

        public IActionResult Index()
        {
            return View();
        }

        public AccountController(
            IAccountRepository accountRepository,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            ApplicationDbContext context
            )
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterModel submittedAccountModel)
        {
            if (ModelState.IsValid)
            {
                Account createdUser = new Account() // Create account object.
                {
                    UserName = submittedAccountModel.UserName,
                    Email = submittedAccountModel.Email,
                };

                Regex regex = new Regex(@"^[a-zA-Z0-9]+$"); // Only letters and digits allowed.
                if (!regex.IsMatch(createdUser.UserName))
                {
                    ModelState.AddModelError("UserName", "Only letters and digits are allowed in the UserName field.");
                }

                IdentityResult result = await _userManager.CreateAsync( // Create the Account-Identity object.
                    createdUser, submittedAccountModel.Password
                    );

                if (result.Succeeded)
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login");
                } else
                {
                    return View(submittedAccountModel);
                }
            } else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(
                loginModel.Username, loginModel.Password, false, false);

            if (result.IsNotAllowed)
            {
                ViewBag.Msg = "Not allowed.";
            }
            if (result.IsLockedOut)
            {
                ViewBag.Msg = "Account is locked out.";
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
