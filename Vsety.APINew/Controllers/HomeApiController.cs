using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Vsety.Application.Services;
using Vsety.Core.Models;
using Vsety.Core.Models.ViewModel;
using Vsety.DataAccess.Entities;
using Vsety.DataAccess.Repositories.Interfaces;

namespace Vsety.APINew.Controllers
{
    [ApiController]
    [Route("homeApi/[controller]")]
    public class HomeApiController : Controller
    {
        private readonly ILogger<HomeApiController> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IPersonsRepository _personsRepository;
        private readonly IPostRepository _postRepository;
        private readonly IImageRepository _imageRepository;

        public HomeApiController(ILogger<HomeApiController> logger, 
            IUsersRepository usersRepository, 
            IPersonsRepository personsRepository,
            IPostRepository postRepository,
            IImageRepository imageRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _personsRepository = personsRepository;
            _postRepository = postRepository;
            _imageRepository = imageRepository;
        }

        [Authorize]
        [HttpGet("Identification")]
        public async Task<IActionResult> Identification()
        {
            string tokenValue = HttpContext.Request.Cookies["token"];
            JwtSecurityToken token1 = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
            var userid = token1.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var userid1 = Guid.Parse(userid);
            UserEntity user = await _usersRepository.GetById(userid1);
            PersonEntity person = await _personsRepository.GetByUserId(userid1);
            ImgEntity img = await _imageRepository.GetByPersonId(person.Id);
            Person person1 = new Person()
            {
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                City = person.City,
                Birthday = person.Birthday,
                Nickname = person.Nickname,
                Description = person.Description,
            };
            User user1 = new User(user.Id, user.Mail, user.PasswordHash, person1);
            IndexViewModel model = new IndexViewModel() { User = user1, };
            return Ok(model);
        }

        [Authorize]
        [HttpGet("GetAvatar")]
        public async Task<IActionResult> GetAvatar(Guid userId)
        {
            UserEntity user = await _usersRepository.GetById(userId);
            PersonEntity person = await _personsRepository.GetByUserId(userId);
            ImgEntity img = await _imageRepository.GetByPersonId(person.Id);

            var memory = new MemoryStream();
            using (var stream = new FileStream(img.Path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "image/jpg", "example.jpg");
        }

        [Authorize]
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        { 
            Post post = new Post()
            {
                Id = Guid.NewGuid(),
                User = model.User,
                Time = DateTime.UtcNow,
                file = model.file,
                Description = model.Description,
            };
            await _postRepository.AddPost(model.User.Id, post);

            return Ok(post);
        }

        [NonAction]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
