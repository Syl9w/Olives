using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Olives.Dtos;
using Olives.Models;

namespace Olives.Services
{
    public interface IImageService
    {
        Task Upload(AddImageDto file, User user);
        Task<Image> GetImage(int userId);
        Task DeleteImage(User user);
    }
}