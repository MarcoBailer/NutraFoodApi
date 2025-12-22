using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nutra.Data;
using Nutra.Interfaces;
using Nutra.Models.Usuario;
using Nutra.Services;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var myNextAppPolicy = "_myNextAppPolicy";

var authority = "https://localhost:7047/";

var connectionString = builder.Configuration
    ["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContextFactory<AlimentosContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // As regras de senha aqui são menos relevantes, pois a senha é gerida no Projeto A,
    // mas vou manter para consistência do objeto User.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<AlimentosContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = authority;
    options.RequireHttpsMetadata = false; // true em produção

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = authority,
        ValidAudience = builder.Configuration["JWT:Audience"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

            var userIdExternal = context.Principal.FindFirstValue("sub");
            var userEmail = context.Principal.FindFirstValue("email");
            var userName = context.Principal.FindFirstValue("name") ?? userEmail;

            if (!string.IsNullOrEmpty(userEmail))
            {
                var user = await userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = userEmail,
                        NomeCompleto = userName,
                        CPF = "",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(user);

                    if (!result.Succeeded)
                    {
                        context.Fail("Falha ao sincronizar usuário lcoal.");
                    }
                }
            }
        }
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myNextAppPolicy,
        policy => policy
            .WithOrigins("http://localhost:3000", builder.Configuration["AppSettings:BaseUrlFront"])
            .AllowAnyHeader()
            .AllowAnyMethod());
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddScoped<IBusca, BuscaService>();
builder.Services.AddScoped<ICalculadoraNutricional, CalculadoraNutricionalService>();
builder.Services.AddScoped<IUserProfile, UserProfileService>();
builder.Services.AddScoped<IAccounts, AccountsService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myNextAppPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
