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
        //private readonly IEventRepository _eventRepository;

        public NewsFeedController(IPostRepository postRepository/*, IEventRepository eventRepository*/)
        {
            _postRepository = postRepository;
            //_eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPosts();
            //var events = _postRepository.GetAllEvents();

            var viewModel = new NewsViewModel
            {
                Posts = posts,
                //Events = events
            };

            return View(viewModel);
        }
    }
}
