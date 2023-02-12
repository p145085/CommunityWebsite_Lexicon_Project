using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? TagId { get; set; }
        public string? Value { get; set; }
        //public List<Post> Posts { get; set; } = new();
    }
}
