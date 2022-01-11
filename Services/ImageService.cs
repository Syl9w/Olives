using System.IO;
using Microsoft.AspNetCore.Mvc;
using Olives.Data;
using Olives.Dtos;
using Olives.Models;

namespace Olives.Services
{
    public class ImageService : IImageService
    {
        private readonly UserContext _context;
        public ImageService(UserContext context)
        {
            _context = context;
        }
        public async void Upload( AddImageDto file)
        {
            if(file.ImageData!=null){
                
                byte[] p1 = null;
                using(var fs1= file.ImageData.OpenReadStream())
                using(var ms1= new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }
                var image = new Image{
                    ImageTitle = file.ImageTitle,
                    ImageData = p1
                };
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}