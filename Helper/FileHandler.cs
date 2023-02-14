using Microsoft.AspNetCore.Hosting;

namespace CommunityWebsite_Lexicon_Project.Helper
{
    public class FileHandler
    {
        private readonly string _webRootPath;

        public FileHandler(IWebHostEnvironment webHostEnvironment)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
        }

        
    }
}
