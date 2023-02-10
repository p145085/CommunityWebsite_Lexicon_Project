using System.Runtime.CompilerServices;

namespace CommunityWebsite_Lexicon_Project.Models
{
    public class ForumThread : Post
    {
        public string? ForumThreadId { get; set; }
        public Account? PostPoster { get; set; }
        public List<Account>? PostParticipants { get; set; }
    }
}
