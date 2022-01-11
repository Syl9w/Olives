using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Olives.Dtos;
using Olives.Services;

namespace Olives.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public UploadImageController(IImageService imageService)
        {
            _imageService = imageService;     
        }
        
        [HttpPost("Upload")]
        public IActionResult Upload([FromForm]AddImageDto file)
        {
            _imageService.Upload(file);
            return Ok();
        }
    }
}