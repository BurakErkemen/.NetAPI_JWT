using CoreLayer.Configuration;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.UnitOfWork;
using DataLayer;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Services;
using SharedLibrary.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        // TokenOptions yapýlandýrmasýný ekleyin
        builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));
        builder.Services.Configure<ClientOptions>(builder.Configuration.GetSection("Clients"));
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
                sqlOptions => sqlOptions.MigrationsAssembly("DataLayer"));
        });



        builder.Services.AddIdentity<UserAppModel, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;

        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


        // 1?? TokenOptions nesnesini al
        var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();

        if (tokenOptions == null)
        {
            throw new Exception("TokenOptions yüklenemedi! Lütfen appsettings.json dosyanýzý kontrol edin.");
        }

        // 2?? DI container içine ekle
        builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience[0],
                IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}