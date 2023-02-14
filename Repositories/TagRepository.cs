using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tag tag)
        {
            await _context.AddAsync(tag);
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public async Task<Tag> GetTagByMatchingTagIdAsync(Guid id)
        {
            return _context.Tags.SingleOrDefault(x => x.TagId == id);
        }

        public List<Tag> GetTagsByMatchingValueSearch(string value)
        {
            return _context.Tags.Where(x => x.Value == value).ToList();
        }
    }
}
