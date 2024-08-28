using Microsoft.AspNetCore.Mvc;
using Vsety.Application.Services;
using Vsety.Core.Models.ViewModel;
using Vsety.Core.Models;
using Vsety.DataAccess.Repositories;
using Vsety.DataAccess;
using Vsety.Infrastructure;

namespace Vsety.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountApiController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPersonsRepository _personsRepository;
        public AccountApiController(ApplicationContext context,
            IUsersRepository usersRepozitory,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IPersonsRepository personsRepository)
        {
            _context = context;
            _usersRepository = usersRepozitory;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _personsRepository = personsRepository;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Autorisation([FromBody] AutorisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
                if (_usersRepository.UserExist(model.Login).Result)
                {
                    var token = await userService.Login(model.Login, model.Password);
                    if (token == "")
                    {
                        ModelState.AddModelError("Password", "Неправильный пароль");
                        return View(model);
                    }
                    HttpContext.Response.Cookies.Append("token", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", "Пользователь с такой почтой не зарегистрирован");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterView([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_usersRepository.UserExist(model.Login).Result)
                {
                    foreach (var person1 in _context.Users)
                    {
                        _context.Remove(person1);
                    }
                    var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
                    await userService.Register(model.Login, model.Password);
                    HttpContext.Response.Cookies.Append("login", model.Login);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("PersonView", "Account");
                }
                else
                {
                    ModelState.AddModelError("Login", "Пользователь с этой почтой уже зарегистрирован");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost("person")]
        public async Task<IActionResult> PersonView([FromBody] Person model)
        {
            if (model.avatar != null)
            {
                model.avatarPath.Path = "/img/" + model.avatar.FileName;
                model.avatarPath.Name = model.avatar.FileName;
            }

            if (ModelState.IsValid)
            {
                foreach (var person in _context.Persons)
                {
                    _context.Remove(person);
                }
                foreach (var person in _context.Imgs)
                {
                    _context.Remove(person);
                }
                await _personsRepository.AddPerson(HttpContext.Request.Cookies["login"], model);


                await _context.SaveChangesAsync();

                return RedirectToAction("Autorisation", "Account");
            }
            return View(model);
        }
    }
}
}
