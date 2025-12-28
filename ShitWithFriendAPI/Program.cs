using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Repositories.Impl;
using ShitWithFriendAPI.Services.Int;
using ShitWithFriendAPI.Services.Impl;
using ShitWithFriendAPI.Middleware;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShitWithFriends API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // 1. DOVE scaricare le chiavi (Useremo l'indirizzo interno HTTP)
    options.Authority = builder.Configuration["Authentication:Authority"];
    options.Audience = builder.Configuration["Authentication:Audience"];
    
    // Disabilita HTTPS per la connessione interna tra container
    options.RequireHttpsMetadata = false;

    // 2. CHI deve aver firmato il token (L'indirizzo pubblico HTTPS)
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        // Qui forziamo l'API ad accettare l'emittente pubblico anche se ci connettiamo internamente
        ValidIssuer = "https://auth.dinonerd.it/realms/ShitWithFriend",
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        NameClaimType = "preferred_username"
    };
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<SWFContext>(DbContextOptions => 
DbContextOptions.UseSqlite(builder.Configuration.GetConnectionString("SWFDbConnectionString")));

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPoopRepository, PoopRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IHighScoreRepository, HighScoreRepository>();
builder.Services.AddScoped<IAchievementRepository, AchievementRepository>();
builder.Services.AddScoped<IUserAchievementRepository, UserAchievementRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPoopService, PoopService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IHighScoreService, HighScoreService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();

var app = builder.Build();

// --- AUTO-MIGRATION ON STARTUP ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SWFContext>();
        // 1. FIX HISTORY: Assicuriamoci che la tabella storia esista
        context.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS ""__EFMigrationsHistory"" (
                ""MigrationId"" TEXT NOT NULL CONSTRAINT ""PK___EFMigrationsHistory"" PRIMARY KEY, 
                ""ProductVersion"" TEXT NOT NULL
            );");
            
        // 2. SKIP INITIAL CREATE: Inseriamo il record per 'InitialCreate' così EF non prova a ricreare le tabelle base (come Games)
        // Nota: Usiamo l'ID della migrazione InitialCreate presente nel progetto
        context.Database.ExecuteSqlRaw(@"
            INSERT OR IGNORE INTO ""__EFMigrationsHistory"" (""MigrationId"", ""ProductVersion"") 
            VALUES ('20251221104258_InitialCreate', '8.0.0');");
            
        // 3. Tenta la migrazione standard (ora dovrebbe saltare InitialCreate e fare solo le nuove)
        context.Database.Migrate();

        Log.Information("Database migrato e allineato correttamente.");
    }
    catch (Exception ex)
    {
        // 4. RESILIENZA: Se l'errore è "Table already exists", significa che la migrazione è parziale. 
        // Ignoriamo l'errore per permettere all'app di avviarsi.
        if (ex.Message.Contains("already exists") || (ex.InnerException != null && ex.InnerException.Message.Contains("already exists")))
        {
            Log.Warning("ATTENZIONE: Trovate tabelle già esistenti. Si prosegue ignorando l'errore di migrazione.");
        }
        else
        {
            // Se è un altro errore, lo logghiamo ma NON blocchiamo l'app in produzione se possibile,
            // oppure rilanciamo se è critico. Per ora logghiamo errore grave.
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Errore durante la migrazione. Tentativo di avvio comunque.");
        }

        // 5. FALLBACK DI SICUREZZA: Eseguiamo comunque le modifiche critiche manuali per l'Avatar
        // nel caso la migrazione AddAvatarColumn_Fix sia fallita a metà.
        try 
        { 
            var context = services.GetRequiredService<SWFContext>();
            context.Database.ExecuteSqlRaw("ALTER TABLE Users ADD COLUMN Avatar TEXT DEFAULT 'DEFAULT_1'");
            context.Database.ExecuteSqlRaw("ALTER TABLE Users ADD COLUMN Email TEXT DEFAULT ''");
        } catch { /* Ignora se colonne esistono */ }
    }
}
// -------------------------------------------------------------


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseMiddleware<UserSyncMiddleware>();
app.UseAuthorization();

app.MapControllers();



app.Run();