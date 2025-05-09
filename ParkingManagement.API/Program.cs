using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ParkingManagement.API.Data;
using ParkingManagement.API.Services;
using ParkingManagement.API.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 🌟 Configuration de la base de données SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🌟 Configuration des paramètres JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");
var issuer = jwtSettings.GetValue<string>("Issuer");
var audience = jwtSettings.GetValue<string>("Audience");

// 🌟 Ajout de l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuerSigningKey = true
        };
    });

// 🌟 Configuration de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// 🌟 Ajout des services dans l'injection de dépendances
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IParkingSpotService, ParkingManagement.API.Services.ParkingSpotService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();



// 🌟 Ajout des contrôleurs et Swagger (Documentation API)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🌟 Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 🚀 Activation de CORS
app.UseCors("AllowAll");

// 🚀 Activation de l'authentification et de l'autorisation
app.UseAuthentication();
app.UseAuthorization();

// 🚀 Mappage des contrôleurs
app.MapControllers();

// 🌟 Démarrage de l'application
app.Run();
