using Vsety.Application.Services;
using Vsety.API.Contracts.Users;

namespace Vsety.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(RegisterUserRequest request,UserService userService)
        {
            await userService.Register(request.Mail, request.Password);

            return Results.Ok();
        }
        private static async Task<IResult> Login(RegisterUserRequest request, UserService userService)
        {
            var token = userService.Login(request.Mail, request.Password);
            return Results.Ok(token);
        }

    }
}
