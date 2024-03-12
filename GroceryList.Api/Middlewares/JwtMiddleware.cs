using GroceryList.Domain.Interfaces.Infra;

namespace GroceryList.Infra.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthService _authService;

        public JwtMiddleware(RequestDelegate next, IAuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            if (token != null)
            {
                var validation = await _authService.ValidateJwtTokenAsync(token);
                if (!validation.IsValid)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                var userId = validation.Claims.FindFirst("sub");
                context.Items["UserId"] = _authService.DecryptUserId(userId.ToString());
            }

            await _next(context);
        }
    }
}
