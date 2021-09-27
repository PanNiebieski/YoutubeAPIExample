using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Yt.ApplicationCore;
using Yt.Models;
using Yt.UI.Models;

namespace Yt.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFinderYouTubeService _finderYouTubeService;
        private YoutubeOptions _youtubeOptions;
        private int _day = -1;
        private List<YtActivityVideoUploadModel> _cache;

        public HomeController(ILogger<HomeController> logger,
             IFinderYouTubeService finderYouTubeService, YoutubeOptions youtubeOptions)
        {
            _logger = logger;
            _finderYouTubeService = finderYouTubeService;
            _youtubeOptions = youtubeOptions;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewData["Title"] = "Youtube aktywność Cezarego Walenciuka";
            ViewData["Description"] = "Tutaj znajduje się lista linków,slajdów,nagrań do prezentacji Cezarego Walenciuka.";

            int day = DateTime.UtcNow.Day;

            if (day != _day && _youtubeOptions.Cache.ToUpperInvariant() == "Y")
            {
                _cache = await _finderYouTubeService.GetActivity(_youtubeOptions.SecretKey)
                .ConfigureAwait(false);
            }

            var activity = _cache;
            return View(activity);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}