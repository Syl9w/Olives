using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Olives.Dtos;

namespace Olives.Services
{
    public interface IImageService
    {
        void Upload(AddImageDto file);
    }
}