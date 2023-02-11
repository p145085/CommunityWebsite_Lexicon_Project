namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Post
    {
        public string? PostId { get; set; }
        public string? Title { get; set; }
        public DateTime CreationDateTime { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Image>? AttachedImages { get; set; }
        public Account? OriginalPoster { get; set; }
        public List<Account>? PostParticipants { get; set; }
        public bool? isEvent { get; set; }
        public bool? isForumThread { get; set; }
        public bool? isBlogPost { get; set; }
        public bool? isReadOnly { get; set; }
        public string? Message { get; set; }
    }
}
