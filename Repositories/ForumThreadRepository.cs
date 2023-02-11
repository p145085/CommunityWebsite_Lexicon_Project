using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class ForumThreadRepository : IForumThreadRepository
    {
        private readonly ApplicationDbContext _context;

        public ForumThreadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(ForumThread forumThread)
        {
            if (!string.IsNullOrEmpty(forumThread.Message))
            {
                _context.ForumThreads.Add(forumThread);
                return _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("You must supply a message.");
            }
        }

        public async Task<ForumThread> GetForumThreadMatchingForumThreadIdAsync(string id)
        {
            return await _context.ForumThreads.SingleOrDefaultAsync(x => x.ForumThreadId == id);
        }

        public List<ForumThread> GetForumThreadsByMatchingAccountUserName(string username)
        {
            return _context.ForumThreads.Where(x => x.OriginalPoster.UserName == username).ToList();
        }

        public List<ForumThread> GetForumThreadsByMatchingEmail(string email)
        {
            return _context.ForumThreads.Where(x => x.OriginalPoster.Email == email).ToList();
        }
    }
}
