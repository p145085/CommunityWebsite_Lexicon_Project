using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostByMatchingPostIdAsync(string id);
        List<Post> GetPostsByMatchingAccountUserName(string username);
        List<Post> GetPostsByMatchingEmail(string email);
        List<Post> GetPostsByMatchingTitleSearch(string titleSearch);
        List<Post> GetPostsBeforeDate(DateTime dateTime);
        List<Post> GetPostsAfterDate(DateTime dateTime);
        List<Post> GetPostsByCreationDateSameDay(DateTime dateTime);
        List<Post> GetPostsContainingTag(string tag);
        List<Post> SearchAllPostsForMessageMatchingQuery(string query);
        List<Account> GetAccountParticipantsInPost(Post post, string id);
        List<Event> GetAllEvents();
        List<ForumThread> GetAllForumThreads();
        List<Post> GetAllPosts();
        Task AddAsync(Post post);
    }
}
