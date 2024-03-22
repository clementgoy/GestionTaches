using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

SeedData.Init();

// Configuration de la base de données SQLite
builder.Services.AddDbContext<BackendContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BackendDatabase")));

// Ajout des contrôleurs
builder.Services.AddControllers();

// Configuration de Swagger avec prise en charge de l'authentification JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mon API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] {}
        }
    });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });


var devCorsPolicy = "devCorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);

}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<JwtTokenValidationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
