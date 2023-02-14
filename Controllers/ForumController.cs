using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class ForumController : Controller
    {
        private UserManager<Account> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IPostRepository _postRepository;
        private ApplicationDbContext _context;

        public ForumController(UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor, IPostRepository postRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
            _context = context;
        }

        [HttpGet("ForumIndex")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Discusser")]
        [HttpPost("PostForumPostForm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostForumPostForm([FromForm] ForumPostViewModel createForumPostViewModel)
        {
            if (ModelState.IsValid)
            {
                Post createdPost = new Post() // #2 - Create "Post"-object.
                {
                    Title = createForumPostViewModel.Title, // User
                    CreationDateTime = DateTime.Now, // Default
                    //Tags = createForumPostViewModel.Tags, // User
                    //AttachedImages = images, // User
                    OriginalPoster = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result, // Default
                    //isEvent = true,
                    isForumThread = true,
                    //isBlogPost = true, // Default 
                    isReadOnly = false, // Default (Admin/Moderator modifies when it is due.)
                    HighlightedDateTime = createForumPostViewModel.HighlightedDateTime, // User
                    Message = createForumPostViewModel.Message // User
                };

                var images = new List<Image>(); // #1 - Handle image(s).
                foreach (var file in createForumPostViewModel.AttachedImages)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            images.Add(new Image
                            {
                                FileName = file.FileName,
                                Data = memoryStream.ToArray(),
                                BelongsToThisPostId = createdPost.PostId
                            });
                        }
                    }
                    foreach (Image image in images)
                    {
                        if (image != null)
                        {
                            await _context.Images.AddAsync(image);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                //foreach (var image in createForumPostViewModel.AttachedImages) // #5 - Create relationship of image(s) to post(s).
                //{
                //    image.BelongsToThisPostId = createdPost.PostId;
                //}

                await _postRepository.AddAsync(createdPost); // #3 - Add "Post"-object to database.

                return RedirectToAction(nameof(Index)); // #4 - Re-direct to Main-page.
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
