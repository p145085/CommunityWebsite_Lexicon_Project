namespace CommunityWebsite_Lexicon_Project.Models
{
    public class Post
    {
        public string? PostId { get; set; }
        public string? PostTitle { get; set; }
        public DateTime PostCreationDateTime { get; set; }
        public List<string>? Tags { get; set; }
        public List<string>? AttachedImages { get; set; }
        public string? PostMessage { get; set; }
    }
}
