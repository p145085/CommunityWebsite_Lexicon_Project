using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite_Lexicon_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<Account>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Event>? Events { get; set; }
        public DbSet<ForumThread>? ForumThreads { get; set; }
    }
}
