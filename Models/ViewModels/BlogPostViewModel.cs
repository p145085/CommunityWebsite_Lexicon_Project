using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class BlogPostViewModel
    {
        //private readonly UserManager<Account> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public string? Title { get; set; }
        //public DateTime CreationDateTime { get; set; } = DateTime.Now; // Should be auto-filled.

        /// public List<Tag> Tags { get; set; } // Comma-separated line of strings.

        [NotMapped]
        public IFormFileCollection? AttachedImages { get; set; } // Bulk-upload images.
        //public List<Image> Images { get; set; }
        public string? OriginalPoster { get; set; } // Auto-set to logged in user.
        //public bool isEvent { get; set; } = false;
        //public bool isForumThread { get; set; } = false;
        public bool isBlogPost { get; set; } = true;
        //public bool isReadOnly { get; set; } = false; 
        public string? Message { get; set; }

        public BlogPostViewModel(/*UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor*/)
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
