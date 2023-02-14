using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Interfaces
{
    public interface ITagRepository
    {
        Task<Tag>? GetTagByMatchingTagIdAsync(Guid id);
        List<Tag> GetTagsByMatchingValueSearch(string value);
        List<Tag> GetAllTags();
        Task AddAsync(Tag tag);
    }
}
