using CommunityWebsite_Lexicon_Project.Data;
using CommunityWebsite_Lexicon_Project.Interfaces;
using CommunityWebsite_Lexicon_Project.Models.BaseModels;
using Microsoft.Extensions.Hosting;

namespace CommunityWebsite_Lexicon_Project.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Image image)
        {
            if (!string.IsNullOrEmpty(image.FileName))
            {
                _context.Images.AddAsync(image);
            }
            else
            {
                throw new Exception("Filename is null.");
            }
        }

        public List<Image> GetAllImages()
        {
            return _context.Images.ToList();
        }

        public async Task<Image> GetImageByMatchingImageIdAsync(Guid id)
        {
            return _context.Images.SingleOrDefault(x => x.ImageId == id);
        }
    }
}
