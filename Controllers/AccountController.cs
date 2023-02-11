using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public AccountController(
            IAccountRepository accountRepository,
            UserManager<Account> userManager,
            SignInManager<Account> signInManager
            )
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel submittedAccountModel)
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

                //try // Setting the password.
                //{
                //    await createdUser.SetPassword(submittedAccountModel.Password);
                //} catch (Exception)
                //{
                //    throw new Exception("Error.");
                //} finally // Verify the password.
                //{
                //    await createdUser.VerifyPassword(submittedAccountModel.PasswordConfirm);
                //}

                IdentityResult result = await _userManager.CreateAsync( // Create the Account-Identity object.
                    createdUser, submittedAccountModel.Password
                    );

                if (result.Succeeded)
                {
                    //await _accountRepository.AddAsync(createdUser); // Save the Account-Identity object. // Samma som _userManager.CreateAsync.
                    return RedirectToAction("Login");
                    //return Ok();
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
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginModel)
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

            // ** START OF DATABASE LOGIN **
            //Account account = await _accountRepository.GetAccountByUsernameAsync(loginModel.Username);

            //if (account == null)
            //{
            //    return BadRequest(new { message = "No account with that username found." });
            //}

            ////Check if the password is correct
            ////if (!VerifyPasswordHash(loginModel.Password, account.Password))
            //if (!await account.VerifyPassword(loginModel.Password))
            //{
            //    return BadRequest(new { message = "Password is incorrect." });
            //}

            //// Generate a JWT token
            ////var token = GenerateToken(account);
            //return Ok(new
            //{
            //    Id = account.Id,
            //    UserName = account.UserName,
            //    Email = account.Email,
            //    //Token = token
            //}); ** END OF DATABASE LOGIN **
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        //private bool VerifyPasswordHash(string password, byte[] passwordHash)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordHash))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != passwordHash[i]) return false;
        //        }
        //    }
        //    return true;
        //}

        //private string GenerateToken(Account account)
        //{
        //    // Your implementation for generating a JWT token goes here
        //}

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
