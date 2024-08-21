using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Vsety.Core.Models;
using Vsety.DataAccess.Repositories;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersRepository _usersRepository;

        public HomeController(ILogger<HomeController> logger, IUsersRepository usersRepository)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            string tokenValue = HttpContext.Request.Cookies["token"];
            JwtSecurityToken token1 = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);
            var userid = token1.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            var userid1 = Guid.Parse(userid);
            var user = _usersRepository.GetById(userid1);
            return View();
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
