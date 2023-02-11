namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Event : Post
    {
        public string? EventId { get; set; }
        public DateTime HighlightedDateTime { get; set; }
        public List<Account>? UsersAttending { get; set; }
    }
}
