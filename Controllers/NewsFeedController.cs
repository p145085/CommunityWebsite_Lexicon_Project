using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.ViewModels;
using CommunityWebsite_Lexicon_Project.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommunityWebsite_Lexicon_Project.Controllers
{
    public class NewsFeedController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITagRepository _tagRepository;

        public NewsFeedController(IPostRepository postRepository, IImageRepository imageRepository, IAccountRepository accountRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _imageRepository = imageRepository;
            _accountRepository = accountRepository;
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPosts();
            var images = _imageRepository.GetAllImages();
            var accounts = _accountRepository.GetAllAccounts();
            var tags = _tagRepository.GetAllTags();

            var viewModel = new NewsViewModel
            {
                Posts = posts,
                Images = images,
                Accounts = accounts,
                Tags = tags
            };

            return View(viewModel);
        }
    }
}
