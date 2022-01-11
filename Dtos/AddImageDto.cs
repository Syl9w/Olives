using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Olives.Dtos
{
    public class AddImageDto
    {
        public string ImageTitle { get; set; }
        public IFormFile ImageData { get; set; }
    }
}