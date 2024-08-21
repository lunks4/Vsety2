using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Vsety.Core.Models;
using Vsety.Core.Models.ViewModel;
using Vsety.DataAccess.Entities;
using Vsety.DataAccess.Repositories;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IPersonsRepository _personsRepository;

        public HomeController(ILogger<HomeController> logger, IUsersRepository usersRepository, IPersonsRepository personsRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
            _personsRepository = personsRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string tokenValue = HttpContext.Request.Cookies["token"];
            JwtSecurityToken token1 = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
            var userid = token1.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var userid1 = Guid.Parse(userid);
            UserEntity user = await _usersRepository.GetById(userid1);
            PersonEntity person = await _personsRepository.GetById((Guid)user.PersonId);
            ImgEntity img = await _personsRepository.GetFileByIdLogo(person.ImgId);
            Person person1 = new Person()
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Gender = person.Gender,
                City = person.City,
                Birthday = person.Birthday,
                Nickname = person.Nickname,
                avatarPath = new Img()
                {
                    Name = img.Name,
                    Path = img.Path,
                }
            };
            User user1 = new User(user.Id, user.Mail, user.PasswordHash, person1);
            IndexViewModel model = new IndexViewModel() { User = user1, };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
