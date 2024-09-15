using Microsoft.AspNetCore.Mvc;
using Vsety.Application.Services;
using Vsety.Core.Models.ViewModel;
using Vsety.Core.Models;
using Vsety.DataAccess;
using Vsety.Infrastructure;
using Vsety.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

namespace Vsety.APINew.Controllers
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
        private readonly IWebHostEnvironment _environment;
        public AccountApiController(ApplicationContext context,
            IUsersRepository usersRepozitory,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            IPersonsRepository personsRepository,
            IWebHostEnvironment environment)
        {
            _context = context;
            _usersRepository = usersRepozitory;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _personsRepository = personsRepository;
            _environment = environment;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Autorisation([FromBody] AutorisationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
                if (_usersRepository.UserExist(model.email).Result)
                {
                    var token = await userService.Login(model.email, model.password);
                    if (token == "")
                    {
                        return BadRequest(new { message = "Неправильный пароль" });
                    }
                    HttpContext.Response.Cookies.Append("token", token);
                    return Ok(new { token });
                }
                else
                {
                    return NotFound(new { message = "User with this email is not registered" });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterView([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_usersRepository.UserExist(model.email).Result)
                {
                    foreach (var person1 in _context.Users)
                    {
                        _context.Remove(person1);
                    }
                    var userService = new UserService(_usersRepository, _passwordHasher, _jwtProvider);
                    await userService.Register(model.email, model.password);
                    HttpContext.Response.Cookies.Append("login", model.email);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "User registered successfully" });
                }
                else
                {
                    return Conflict(new { message = "User with this email already registered" });
                }
            }
            return BadRequest(model);
        }

        [Authorize]
        [HttpPost("person")]
        public async Task<IActionResult> PersonView(Person model)
        {
            if (ModelState.IsValid)
            {
                
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

                // Проверяем, что заголовок не пустой и начинается с "Bearer"
                if (authHeader != null && authHeader.StartsWith("Bearer"))
                {
                    // Извлекаем сам токен
                    var token = authHeader.Substring("Bearer ".Length).Trim();

                    // Логика обработки токена (если нужно)
                    // Например, можно проверить токен или декодировать его
                    // Далее выполняем логику обработки данных формы
                    // ...

                    JwtSecurityToken token1 = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    await _personsRepository.AddPerson(Guid.Parse(token1.Claims.FirstOrDefault(c => c.Type == "userId")?.Value), model);
                }
                

                await _context.SaveChangesAsync();

                return Ok(new { message = "Person details saved successfully" });
            }
            return BadRequest(model);
        }
    }
}

