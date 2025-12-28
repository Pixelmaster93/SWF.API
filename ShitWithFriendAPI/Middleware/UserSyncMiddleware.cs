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
            try
            {
                if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
                {
                    // 1. Estrai l'UUID sicuro dal Token (Keycloak 'sub')
                    var userIdString = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                                       ?? context.User.FindFirst("sub")?.Value;

                    // 2. Estrai username per info (ma comanda l'ID)
                    var username = context.User.FindFirst("preferred_username")?.Value
                                   ?? context.User.Identity.Name ?? "Unknown";

                    if (Guid.TryParse(userIdString, out var userId))
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<SWFContext>();

                            // 3. Cerchiamo l'utente per ID (non per username!)
                            var user = await dbContext.Users.FindAsync(userId);
                            if (user == null)
                            {
                                // CREATE: Usiamo l'ID di Keycloak
                                user = new User
                                {
                                    Id = userId, // <--- FORZATURA ID
                                    Username = username
                                };
                                dbContext.Users.Add(user);
                                await dbContext.SaveChangesAsync();
                            }
                            else if (user.Username != username)
                            {
                                // UPDATE: Se ha cambiato nome su Keycloak, aggiorniamo il DB locale
                                user.Username = username;
                                await dbContext.SaveChangesAsync();
                            }
                        }
                    }
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                 var logger = context.RequestServices.GetService<ILogger<UserSyncMiddleware>>();
                 logger?.LogError(ex, "Errore critico durante la sincronizzazione utente (UserSyncMiddleware).");
                 
                 context.Response.StatusCode = 500;
                 await context.Response.WriteAsync("Errore interno del server durante la sincronizzazione utente.");
                 // Non chiamiamo _next(context) per interrompere la pipeline in caso di errore critico auth
            }
        }
    }
}
