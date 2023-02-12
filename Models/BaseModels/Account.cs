using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Account : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new Guid? Id { get; set; }
        //public List<Post> ParticipatesIn { get; set; } = new();
        //public List<Post> WillBeAttendingTo { get; set; } = new();
        //public List<Post> OriginalPosterOf { get; set; } = new();

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
