using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using IconosGeograficos.Data;
using Microsoft.EntityFrameworkCore;
using IconosGeograficos.Repositorios.Implementa;
using IconosGeograficos.Repositorios;
using IconosGeograficos.Servicios;
using IconosGeograficos.Servicios.Implementa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IconosGeograficos.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions .ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Pre-Aceleracion Ramiro API Geografica", Version = "v1" });

    //Seguridad
    o.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingrese el 'Bearer [Token]' para poder autentificarse dentro de la aplicacion"
        });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new List<string>()
        }
    });
});

var geoConn = builder.Configuration.GetConnectionString("APIGeoConnection");
builder.Services.AddDbContext<GeoContext>(x => x.UseSqlServer(geoConn));
var usuConn = builder.Configuration.GetConnectionString("APIUsuConnection");
builder.Services.AddDbContext<UsuContext>(x => x.UseSqlServer(usuConn));

//JWT
builder.Services.AddIdentity<Usuario, IdentityRole>()
       .AddEntityFrameworkStores<UsuContext>()
       .AddDefaultTokenProviders();

//ID para implementar el sistema de login y el de registro
builder.Services.AddAuthentication(configureOptions: options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "https://localhost:44353", //Cambiar esto por el localhost que tengas
        ValidIssuer = "https://localhost:44353", //Cambiar esto por el localhost que tengas
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s:"KeySecretaSuperLargaDeAUTORIZACION"))
    };
});

// Repositorios
builder.Services.AddScoped<IiconoGeograficoRepositorio, IconoGeograficoRepositorio>();
builder.Services.AddScoped<ICiudadesRepositorio, CiudadesRepositorio>();
//builder.Services.AddTransient<IMailRepository, MailRepository>();

// Services
builder.Services.AddScoped<IiconoGeograficoServicio, IconoGeograficoServicio>();
builder.Services.AddScoped<ICiudadesServicio, CiudadesServicio>();
//builder.Services.AddTransient<IMailService, MailService>();

//build api
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
