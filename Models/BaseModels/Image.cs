using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? ImageId { get; set; }
        public string? Path { get; set; }
        public string? FileName { get; set; }
        public byte[]? Data { get; set; }
        public Post? BelongsTo { get; set; }
    }
}
