using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olives.Data;
using Olives.Dtos;
using Olives.Models;

namespace Olives.Services
{
    public class ImageService : IImageService
    {
        private readonly UserContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ImageService(UserContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task Upload(AddImageDto file, User user)
        {
            if (file.ImageData != null)
            {

                byte[] p1 = null;
                using (var fs1 = file.ImageData.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    await fs1.CopyToAsync(ms1);
                    p1 = ms1.ToArray();
                }
                var image = new Image
                {
                    ImageTitle = file.ImageTitle,
                    ImageData = p1,
                    UserId = user.Id
                };

                _context.Images.Add(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Image> GetImage(int userId)
        {
            var img = await _context.Images.FirstOrDefaultAsync(i => i.UserId == userId);
            return img;
        }

        public async Task DeleteImage(User user)
        {
            Image img = await _context.Images.FirstOrDefaultAsync(i => i.UserId == user.Id);
            if (img != null){
                _context.Images.Remove(img);
                await _context.SaveChangesAsync();
            }
        }   
    }
}