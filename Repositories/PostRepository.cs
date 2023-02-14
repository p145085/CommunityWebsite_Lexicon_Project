using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Post post)
        {
            try
            {
                if (!string.IsNullOrEmpty(post.Message))
                {
                    _context.Posts.AddAsync(post);
                }
                else
                {
                    throw new Exception("You must supply a message.");
                }
            } catch (Exception ex)
            {
                throw new Exception("Could not add post to database. " + ex.Message);
            }
            
        }

        //public List<Account> GetAccountParticipantsInPost(Post post, Guid id)
        //{
        //    return post.Participants.ToList();
        //}

        //public List<Event> GetAllEvents()
        //{
        //    return _context.Events.ToList();
        //}

        //public List<ForumThread> GetAllForumThreads()
        //{
        //    return _context.ForumThreads.ToList();
        //}

        public async Task<Post>? GetPostByMatchingPostIdAsync(Guid id)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.PostId == id);
        }

        public List<Post> GetPostsAfterDate(DateTime dateTime)
        {
            return _context.Posts.Where(x => x.CreationDateTime > dateTime).ToList();
        }

        public List<Post> GetPostsBeforeDate(DateTime dateTime)
        {
            return _context.Posts.Where(x => x.CreationDateTime < dateTime).ToList();
        }

        public List<Post> GetPostsByCreationDateSameDay(DateTime dateTime)
        {
            return _context.Posts.Where(x => x.CreationDateTime == dateTime).ToList(); // Match specific time of day? If yes, fix.
        }

        public List<Post> GetPostsByMatchingAccountUserName(string username)
        {
            return _context.Posts.Where(x => x.OriginalPoster.UserName == username).ToList();
        }

        public List<Post> GetPostsByMatchingEmail(string email)
        {
            return _context.Posts.Where(x => x.OriginalPoster.Email == email).ToList();
        }

        public List<Post> GetPostsByMatchingTitleSearch(string titleSearch)
        {
            return _context.Posts.Where(x => x.Title == titleSearch).ToList();
        }

        //public List<Post> GetPostsContainingTag(string tag)
        //{
        //    //return _context.Posts.Where(x => x.Tags.Contains(tag.Value)).ToList();
        //    //return _context.Posts.Where(x => x.Tags.Contains(tag))
        //    return _context.Posts.Where(x => x.Tags.Any(t => t.Value == tag)).ToList();
        //}

        public List<Post> SearchAllPostsForMessageMatchingQuery(string query)
        {
            return _context.Posts.Where(x => x.Message.Contains(query)).ToList();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
    }
}
