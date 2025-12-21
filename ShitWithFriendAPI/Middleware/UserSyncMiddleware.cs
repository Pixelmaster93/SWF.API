using System.Security.Claims;
using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Middleware
{
    public class UserSyncMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public UserSyncMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? context.User.FindFirst("sub")?.Value;

                var username = context.User.FindFirst("preferred_username")?.Value
                               ?? context.User.Identity.Name
                               ?? "Unknown Pooper";

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    // Creiamo uno scope dedicato per il DB per evitare conflitti con la richiesta principale
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<SWFContext>();

                        var user = await dbContext.Users.FindAsync(userId);
                        if (user == null)
                        {
                            // L'utente non esiste, creiamolo!
                            user = new User
                            {
                                Id = userId,
                                Username = username
                            };
                            dbContext.Users.Add(user);
                            await dbContext.SaveChangesAsync();
                        }
                        else if (user.Username != username)
                        {
                            // Aggiorniamo il nickname se è cambiato
                            user.Username = username;
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            // Passa la palla al prossimo step (il Controller)
            await _next(context);
        }
    }
}
