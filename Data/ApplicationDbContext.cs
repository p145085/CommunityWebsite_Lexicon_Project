using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Post>? Posts { get; set; }
        //public DbSet<Event>? Events { get; set; }
        //public DbSet<ForumThread>? ForumThreads { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Tag>? Tags { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //    //modelBuilder.Entity<Post>(cfg => {
        //    //    cfg.HasOne(p => p.OriginalPoster)
        //    //    .WithMany(a => a.OriginalPosterOf)
        //    //    .HasForeignKey(p => p.OriginalPosterId);

        //    //    cfg.HasMany(p => p.Participants)
        //    //        .WithMany(a => a.ParticipatesIn);
        //    //    cfg.HasMany(p => p.UsersAttending)
        //    //        .WithMany(a => a.WillBeAttendingTo);
        //    //});
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(e => new { e.LoginProvider, e.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });


            //modelBuilder.Entity<Post>()
            //    .HasMany(p => p.Tags)
            //    .WithOne(t => t.RelatedToThisPost)
            //    .HasForeignKey(t => t.TagId);

            //modelBuilder.Entity<Post>()
            //    .HasMany(p => p.AttachedImages)
            //    .WithOne(i => i.BelongsToThisPostId)
            //    .HasForeignKey(i => i.ImageId);
        }
    }
}
