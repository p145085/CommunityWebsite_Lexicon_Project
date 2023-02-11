using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Account : IdentityUser
    {
        public async Task SetPassword(string password)
        {
            PasswordHash = new PasswordHasher<Account>().HashPassword(this, password);
        }

        public async Task<bool> VerifyPassword(string password)
        {
            var result = new PasswordHasher<Account>().VerifyHashedPassword(this, PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
