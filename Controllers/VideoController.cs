using Microsoft.AspNetCore.Mvc;

namespace VideoMaker.Controllers
{
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;

        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(1024 * 1024 * 1024)]
        public async Task<IActionResult> TransformVideos(List<IFormFile> files)
        {


            return Json(new { files = "ok" });
        }
    }
}
