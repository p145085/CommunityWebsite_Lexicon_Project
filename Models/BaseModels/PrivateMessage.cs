namespace CommunityWebsite_Lexicon_Project.Models.BaseModels
{
    public class PrivateMessage
    {
        public string? PrivateMessageId { get; set; }
        public Account? PrivateMessageSender { get; set; }
        public Account? PrivateMessageReceiver { get; set; }
        public string? Message { get; set; }
    }
}
