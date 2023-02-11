using Microsoft.AspNetCore.Mvc;

namespace CommunityWebsite_Lexicon_Project.Models.ViewModels
{
    public class ErrorViewModel : Controller
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
