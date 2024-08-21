using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess;
using Vsety.Core.Models;
using Vsety.Core.Models.ViewModel;
using Org.BouncyCastle.Asn1.Ocsp;
using Vsety.Application.Services;
using Vsety.DataAccess.Repositories;
using Vsety.Infrastructure;
using System.Xml;


namespace Vsety.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPersonsRepository _personsRepository;
        public AccountController(ApplicationContext context, 
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
        [HttpGet]
        public IActionResult Autorisation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autorisation(AutorisationViewModel model)
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

        [HttpGet]
        public IActionResult RegisterView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterView(RegisterViewModel model)
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

        [HttpGet]
        public IActionResult PersonView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PersonView(Person model)
        {
            if (model.avatar != null)
            {
                model.avatarPath.Path = "/img/" +  model.avatar.FileName;
                model.avatarPath.Name = model.avatar.FileName;
            } 
            
            if (ModelState.IsValid)
            {
                foreach(var person in _context.Persons)
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
