using System.Security.Claims;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Middleware
{
    public class UserSyncMiddleware
    {
        private readonly RequestDelegate _next;

        public UserSyncMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Usually 'sub' mapped to NameIdentifier
                var usernameClaim = context.User.FindFirst("preferred_username")?.Value;

                if (userIdClaim != null && usernameClaim != null && Guid.TryParse(userIdClaim, out var userId))
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                        await userService.SyncUser(userId, usernameClaim);
                    }
                }
            }

            await _next(context);
        }
    }
}
