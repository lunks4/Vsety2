using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess;
using Vsety.Core.Models;
using Vsety.API.Contracts.Users;
using Vsety.Core.Models.ViewModels;
using Vsety.API.Endpoints;
using Org.BouncyCastle.Asn1.Ocsp;
using Vsety.Application.Services;
using Vsety.DataAccess.Repositories;
using Vsety.Infrastructure;


namespace Vsety.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public AccountController(ApplicationContext context, 
            IUsersRepository usersRepozitory, 
            IPasswordHasher passwordHasher, 
            IJwtProvider jwtProvider)
        {
            _context = context;
            _usersRepository = usersRepozitory;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
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
                //_context.Add(model);
                //await _context.SaveChangesAsync();
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
            if (ModelState.IsValid)
            {
                //_context.Add(model);
                //await _context.SaveChangesAsync();
                foreach(var person in _context.Persons)
                {
                    _context.Remove(person);
                }

                var user = _context.Users.FirstOrDefault(u => u.Mail == HttpContext.Request.Cookies["login"]);
                //user.Person = model; 
                _context.Add(model);

                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }


}
