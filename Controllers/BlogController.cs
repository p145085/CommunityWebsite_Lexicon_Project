using Microsoft.AspNetCore;
using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Image = CommunityWebsite_Lexicon_Project.Models.BaseModels.Image;
using Microsoft.AspNetCore.Authorization;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class BlogController : Controller
    {
        private UserManager<Account> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext _context;
        private readonly string _webRootPath;
        private IPostRepository _postRepository;
        private IImageRepository _imageRepository;
        private ITagRepository _tagRepository;
        private readonly SignInManager<Account> _signInManager;

        public BlogController(UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IPostRepository postRepository, IImageRepository imageRepository, ITagRepository tagRepository, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _webRootPath = webHostEnvironment.WebRootPath;
            _postRepository = postRepository;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _signInManager = signInManager;
        }

        public bool DoesDirectoryExist(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UploadFilesToDisk(IFormFileCollection files)
        {
            if (DoesDirectoryExist(_webRootPath + "\\uploaded_images"))
            {
                foreach (var file in files)
                {
                    var filePath = Path.Combine(_webRootPath, "uploaded_images", file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            } else
            {
                Directory.CreateDirectory(_webRootPath + "\\uploaded_images");
                foreach (var file in files)
                {
                    var filePath = Path.Combine(_webRootPath, "uploaded_images", file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }

        [HttpGet("BlogIndex")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Blogger")]
        [HttpPost("PostBlogPostForm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostBlogPostForm([FromForm] BlogPostViewModel createBlogPostViewModel)
        {
            if (ModelState.IsValid)
            {

                var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
                var userId = await _userManager.GetUserIdAsync(user);


                Post createdPost = new Post() // #2 - Create "Post"-object.
                {
                    PostId = Guid.NewGuid(),
                    Title = createBlogPostViewModel.Title, // User
                    CreationDateTime = DateTime.Now, // Default
                    //Tags = createBlogPostViewModel.Tags, // User
                    //AttachedImages = images, // User
                    OriginalPoster = user, // Default
                    OriginalPosterId = userId,
                    //isEvent = createBlogPostViewModel.isEvent,
                    //isForumThread = createBlogPostViewModel.isForumThread,
                    isBlogPost = true, // Default 
                    isReadOnly = false, // Default (Admin/Moderator modifies when it is due.)
                    //HighlightedDateTime = createBlogPostViewModel.HighlightedDateTime, // User
                    Message = createBlogPostViewModel.Message // User
                };

                try
                {
                    await UploadFilesToDisk(createBlogPostViewModel.AttachedImages); // Upload submitted image(s).
                } catch (Exception ex)
                {
                    throw new Exception("Error uploading files to server. " + ex.Message);
                };

                try
                {
                    await _postRepository.AddAsync(createdPost); // #3 - Add "Post"-object to database.
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving post to database. " + ex.Message);
                };

                List<Image> images = new List<Image>(); // #1 - Handle image(s).
                try
                {
                    foreach (IFormFile file in createBlogPostViewModel.AttachedImages) // For every submitted image ->
                    {
                        //using (var stream = file.OpenReadStream())
                        //{
                        //    using (var memoryStream = new MemoryStream())
                        //    {
                        //        await stream.CopyToAsync(memoryStream);
                        //        images.Add(new Image
                        //        {
                        //            FileName = file.FileName,
                        //            Data = memoryStream.ToArray(),
                        //            BelongsToThisPostId = createdPost.PostId
                        //        });
                        //    }
                        //}

                        images.Add(new Image() // Create a "Image"-object and set info + relation.
                        {
                            FileName = file.FileName,
                            BelongsToThisPostId = createdPost.PostId,
                        });
                    };
                } catch (Exception ex)
                {
                    throw new Exception("Error constructing 'Image'-objects. " + ex.Message);
                }
                
                try
                {
                    foreach (Image image in images) // For every created "Image"-object save it to database one-by-one.
                    {
                        if (image != null)
                        {
                            await _imageRepository.AddAsync(image);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return null;
                        }
                    };
                } catch (Exception ex)
                {
                    throw new Exception("Error saving 'Image'-objects to database. " + ex.Message);
                }

                List<Tag> tags = new List<Tag>();
                try
                {
                    string text = createBlogPostViewModel.Tags;
                    text = text.ToUpper();
                    text = text.Normalize();
                    string[] separators = new string[] { "," };
                    string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string theTag in words)
                    {
                        Tag newTag = new Tag();
                        newTag.Value = theTag;
                        newTag.RelatedToThisPost = createdPost.PostId;
                        tags.Add(newTag);
                    }
                    createdPost.Tags = tags; // Add the tags to the "Post"-object.

                    try
                    {
                        foreach (Tag tag in tags)
                        {
                            _tagRepository.AddAsync(tag); // Add the tags to the database.
                        }
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not add tags to database. " + ex.Message);
                    }
                } catch(Exception ex)
                {
                    throw new Exception("Encountered an error handling the tag(s). " + ex.Message);
                }
                
                return RedirectToAction(nameof(Index)); // #4 - Re-direct to Main-page.
            }
            else
            {
                throw new Exception("Modelstate was not valid.");
            }
        }

        //Edit
        //Delete
        //Read => NewsFeedController.cs.
    }
}
