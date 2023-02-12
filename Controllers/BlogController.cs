using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using CommunityWebsite_Lexicon_Project.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class BlogController : Controller
    {
        private UserManager<Account> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IPostRepository _postRepository;
        private ApplicationDbContext _context;

        public BlogController(UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor, IPostRepository postRepository, ApplicationDbContext context)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
            _context = context;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("PostBlogPostForm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostBlogPostForm([FromForm] BlogPostViewModel createBlogPostViewModel)
        {
            if (ModelState.IsValid)
            {
                var images = new List<Image>();
                foreach (var file in createBlogPostViewModel.AttachedImages)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            images.Add(new Image
                            {
                                FileName = file.FileName,
                                Data = memoryStream.ToArray()
                            });
                        }
                    }
                }

                Post createdPost = new Post()
                {
                    Title = createBlogPostViewModel.Title,
                    CreationDateTime = DateTime.Now,
                    //Tags = createBlogPostViewModel.Tags,
                    AttachedImages = images,
                    OriginalPoster = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result,
                    //isEvent = createBlogPostViewModel.isEvent,
                    //isForumThread = createBlogPostViewModel.isForumThread,
                    //isBlogPost = createBlogPostViewModel.isBlogPost,
                    //isReadOnly = createBlogPostViewModel.isReadOnly,
                    Message = createBlogPostViewModel.Message
                };

                await _postRepository.AddAsync(createdPost);
                //await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                //return View(createdPost);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
