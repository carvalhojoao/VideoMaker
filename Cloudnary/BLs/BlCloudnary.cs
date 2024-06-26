﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Cloudnary.BLs
{
    public class BlCloudnary
    {
        private Cloudinary _cloudinary;
        public BlCloudnary(IConfiguration configuration)
        {
            var cloudName = configuration.GetSection("AppSettings:cloudName").Value;
            var cloudApiKey = configuration.GetSection("AppSettings:cloudApiKey").Value;
            var cloudApiSecret = configuration.GetSection("AppSettings:cloudApiSecret").Value;
            Account account = new Account(cloudName, cloudApiKey, cloudApiSecret);

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task UploadVideo(string name, Stream file)
        {
            try
            {
                var uploadParams = new VideoUploadParams
                {
                    EagerAsync = true,
                    File = new FileDescription(name + "_TikTok", file),
                    PublicId = name + "_TikTok",
                    Transformation = new Transformation()
                      .Background("blurred:2000:0").Height(1920).Width(1080).Crop("pad").Chain()
                      .Duration("60").StartOffset("0").Chain()
                      .Effect("saturation:25").Chain()
                      .Effect("contrast:15").Chain()
                      .FetchFormat("mp4")
                }; 

                _cloudinary.Api.Timeout = 60000;
                var result = await _cloudinary.UploadAsync(uploadParams);
                Console.WriteLine(result.Url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }

        }

        //cloudinary.Api.UrlVideoUp.Transform(new Transformation().Background("blurred:2000:0").Height(1920).Width(1080).Crop("pad")).BuildVideoTag("samples/cld-sample-video")
    }
}
