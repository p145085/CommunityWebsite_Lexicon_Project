using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class ForumThreadPostViewModel
    {
        public string Title { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
        //public List<Tag>? Tags { get; set; }
        [NotMapped]
        public IFormFileCollection? AttachedImages { get; set; }
        public string OriginalPoster { get; set; }
        public bool? isEvent { get; set; }
        public bool? isForumThread { get; set; } = true;
        public bool? isBlogPost { get; set; }
        public bool? isReadOnly { get; set; }
        public DateTime? HighlightedDateTime { get; set; }
        public string Message { get; set; }

        public ForumThreadPostViewModel(/*UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor*/)
        {
            //_userManager = userManager;
            //_httpContextAccessor = httpContextAccessor;

            //Account op = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            //OriginalPoster = op.UserName;

            //foreach(Image imgDetails in AttachedImages)
            //{
            //    return imgDetails.Path;
            //}
        }
    }
}
