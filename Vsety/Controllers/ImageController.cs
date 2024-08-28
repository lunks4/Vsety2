using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vsety.API.Controllers
{
    [ApiController]
    [Route("api/Images")]
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine("wwwroot/img", imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var image = System.IO.File.OpenRead(imagePath);
            var contentType = "image/jpg"; // Используйте правильный MIME тип для вашего изображения

            return File(image, contentType);
        }

        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                var filePath = Path.Combine(uploadsFolder, imageFile.FileName);

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return Ok(new { message = "Image uploaded successfully", path = "/img/" + imageFile.FileName });
            }

            return BadRequest(new { message = "No file was uploaded" });
        }

    }
}
