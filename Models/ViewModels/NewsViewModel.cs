using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class NewsViewModel
    {
        public List<Post>? Posts { get; set; }
        public List<Image>? Images { get; set; }
        public List<Account>? Accounts { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
