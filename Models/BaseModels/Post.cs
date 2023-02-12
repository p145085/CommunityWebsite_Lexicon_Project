using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? PostId { get; set; }
        [Required]
        public string? Title { get; set; }
        public DateTime CreationDateTime { get; set; }
        //public List<Tag> Tags { get; set; } = new();
        public List<Image>? AttachedImages { get; set; }
        //[ForeignKey("Account")]
        //public Guid OriginalPosterId { get; set; }
        public Account? OriginalPoster { get; set; }
        //public List<Account> Participants { get; set; } = new();
        //public List<Account> UsersAttending { get; set; } = new();

        public bool isEvent { get; set; }
        public bool isForumThread { get; set; }
        public bool isBlogPost { get; set; }
        public bool isReadOnly { get; set; }
        public DateTime HighlightedDateTime { get; set; }

        [Required]
        public string? Message { get; set; }
    }
}
