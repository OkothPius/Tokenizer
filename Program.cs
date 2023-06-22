//using DAL.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Services;
//using DAL_TestDB.Data;

using DAL.Data;
using WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddSingleton<DatabaseContext>(provider => new DatabaseContext(connectionString));
//builder.Services.AddScoped<DatabaseContext>();

//Configure JWT authentication
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);


builder.Services.AddScoped<Tokenizer>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:44319")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Configurations for the Authorization of API

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Evoucher API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    new string[] { }
//                }
//            });
//});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(
    c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EvcAPI v1");
    });


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
