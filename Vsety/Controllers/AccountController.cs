using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vsety.DataAccess;
using Vsety.Core.Models;
using Vsety.API.Contracts.Users;
using Vsety.Core.Models.ViewModels;


namespace Vsety.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
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
                RegisterUserRequest reguest = new RegisterUserRequest(Mail:model.Login, Password:model.Password);
                //UsersEndpoints.Register(reguest, new UserService(new UsersRepository(_context)));
                HttpContext.Response.Cookies.Append("login", model.Login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Person", "Account");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Person()
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
                user.Person = model;
                _context.Add(model);

                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }


}
