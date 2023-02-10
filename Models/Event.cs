namespace CommunityWebsite_Lexicon_Project.Models
{
    public class Event : Post
    {
        public string? EventId { get; set; }
        public DateTime HighlightedDateTime { get; set; }
        public List<Account>? UsersAttending { get; set; }
    }
}
