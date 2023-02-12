using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class PrivateMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? PrivateMessageId { get; set; }
        //public Account? PrivateMessageSender { get; set; }
        //public Account? PrivateMessageReceiver { get; set; }
        public string? Message { get; set; }
    }
}
