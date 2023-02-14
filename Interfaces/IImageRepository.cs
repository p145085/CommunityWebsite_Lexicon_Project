using CommunityWebsite_Lexicon_Project.Models.BaseModels;

namespace CommunityWebsite_Lexicon_Project.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> GetImageByMatchingImageIdAsync(Guid id);
        List<Image> GetAllImages();
        Task AddAsync(Image image);
    }
}
