﻿using Cloudnary.BLs;
using Microsoft.AspNetCore.Mvc;

namespace VideoMaker.Controllers
{
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private readonly BlCloudnary _blCloudnary;
        public VideoController(ILogger<VideoController> logger, BlCloudnary blCloudnary)
        {
            _logger = logger;
            _blCloudnary = blCloudnary;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(40000000)]
        public async Task<IActionResult> TransformVideos(IFormFile file)
        {
            Stream videoStream = file.OpenReadStream();
            await _blCloudnary.UploadVideo(file.FileName.Split('.')[0], videoStream);
            return Json(new { files = "ok" });
        }
    }
}
