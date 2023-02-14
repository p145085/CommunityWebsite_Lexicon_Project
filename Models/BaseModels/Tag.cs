using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? TagId { get; set; }
        [Required]
        public string Value { get; set; }
        public Guid? RelatedToThisPost { get; set; }
    }
}
