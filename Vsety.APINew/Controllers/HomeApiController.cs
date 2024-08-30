using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        [HttpGet("GetPerson")]
        public async Task<IActionResult> GetPersonByUserId(Guid userId)
        {
            UserEntity user = await _usersRepository.GetById(userId);
            PersonEntity person = await _personsRepository.GetByUserId(userId);
            ImgEntity img = await _imageRepository.GetByPersonId(person.Id);


            return Ok(person);
        }

        [Authorize]
        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts(int count)
        {
            var posts = await _postRepository.GetAllPosts(count);

            return Ok(posts);
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

            return File(memory, _imageRepository.GetContentType(img.Path), "Avatar.jpg");
        }

        [Authorize]
        [HttpGet("GetPostFile")]
        public async Task<IActionResult> GetPostFile(Guid imgId)
        {
            ImgEntity img = await _imageRepository.GetById(imgId);

            var memory = new MemoryStream();
            using (var stream = new FileStream(img.Path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, _imageRepository.GetContentType(img.Path), img.Id + ".jpg");
        }

        
        [NonAction]
        public async Task<IActionResult> GetFile(Guid imgId)
        {
            ImgEntity img = await _imageRepository.GetById(imgId);

            var memory = new MemoryStream();
            using (var stream = new FileStream(img.Path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, _imageRepository.GetContentType(img.Path), img.Id + ".jpg");
        }

        //[Authorize]
        //[HttpGet("GetAllPosts")]
        //public async Task<IActionResult> GetAllPosts(int count)
        //{
        //    UserEntity user = await _usersRepository.GetById(userId);
        //    PersonEntity person = await _personsRepository.GetByUserId(userId);
        //    ImgEntity img = await _imageRepository.GetByPersonId(person.Id);

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(img.Path, FileMode.Open))
        //    {
        //        stream.CopyTo(memory);
        //    }
        //    memory.Position = 0;

        //    return File(memory, _imageRepository.GetContentType(img.Path), "Avatar.jpg");
        //}

        [Authorize]
        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            UserEntity user = await _usersRepository.GetById(model.UserId);
            Post post = new Post()
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Description = model.Description,
                file = model.file,
            };
            PostEntity postEntity = await _postRepository.AddPost(model.UserId, post);

            return Ok(postEntity);
        }


        [Authorize]
        [HttpPatch("AddCommentary")]
        public async Task<IActionResult> AddCommentary(AddCommentaryViewModel model)
        {
            UserEntity user = await _usersRepository.GetById(model.UserId);
            PostEntity post = await _postRepository.GetById(model.PostId);

            var comment = await _postRepository.AddCommentary(post, user, model.Comment);

            return Ok(comment);

        }

        [Authorize]
        [HttpPatch("AddLike")]
        public async Task<IActionResult> AddLike(AddCommentaryViewModel model)
        {
            UserEntity user = await _usersRepository.GetById(model.UserId);
            PostEntity post = await _postRepository.GetById(model.PostId);

            var post1 = await _postRepository.AddLike(post, user);

            return Ok(post1);
        }

        [Authorize]
        [HttpPatch("AddRepost")]
        public async Task<IActionResult> AddRepost(AddCommentaryViewModel model)
        {
            UserEntity user = await _usersRepository.GetById(model.UserId);
            PostEntity post = await _postRepository.GetById(model.PostId);

            var post1 = await _postRepository.AddRepost(post, user);

            return Ok(post1);
        }

        [NonAction]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
