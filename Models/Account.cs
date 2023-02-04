using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models
{
    public class Account : IdentityUser
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        private string? Password { get; set; }
        [Required]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        private string? PasswordConfirm { get; set; }
        public async Task SetPassword(string password)
        {
            Password = new PasswordHasher<Account>().HashPassword(this, password);
        }

        public async Task<bool> VerifyPassword(string password)
        {
            var result = new PasswordHasher<Account>().VerifyHashedPassword(this, Password, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
