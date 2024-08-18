//using Carter;
//using EventEchosAPI.Database;
//using FluentValidation;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//// Assembly
//var assembly = typeof(Program).Assembly;

//// Database Connection
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
//});

//// Add Automapper
////builder.Services.

//// Request timeout
//builder.Services.AddRequestTimeouts();

//// Authentication & Authorization
//// Secret Key
//var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    // Configure JWT Authentication Parameters
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
//    };
//});

//builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

//// MediatR
//builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

//// FluentValidation
//builder.Services.AddValidatorsFromAssembly(assembly);

//// Carter
//builder.Services.AddCarter();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.EnableAnnotations();
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "Event Echos API",
//        Description = "New API Project"
//    });

//    // Add security definition for Bearer token
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {

//        Type = SecuritySchemeType.ApiKey,
//        Name = "Authorization",
//        Description = "Bearer token. Write Bearer [space] and token to the header. \r\n\r\n Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXV4",
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header
//    });

//    // Add security requirement for Bearer token
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                },
//                Scheme = "oauth2",
//                Name = "Bearer",
//                In = ParameterLocation.Header,
//            },
//            new List<string>()
//        }
//    });
//    ;
//});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigins",
//        builder =>
//        {
//            builder.WithOrigins("*")
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});


//var app = builder.Build();

//// Carter
//app.MapCarter();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

//app.UseCors("AllowSpecificOrigins");

//app.Run();

using Carter;
using EventEchosAPI.Database;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Assembly
var assembly = typeof(Program).Assembly;

// Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection"));
}, ServiceLifetime.Transient);

// Add Automapper (uncomment if using AutoMapper)
// builder.Services.AddAutoMapper(assembly);

// Request timeout (uncomment if using Request Timeout feature)
// builder.Services.AddRequestTimeouts();

// Authentication & Authorization
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
if (string.IsNullOrEmpty(key))
{
    throw new ArgumentException("API secret key is missing in configuration.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});

// Add JSON and XML serialization formatters
builder.Services.AddControllers()
    .AddNewtonsoftJson() // For JSON serialization
    .AddXmlDataContractSerializerFormatters(); // For XML serialization

// MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(assembly);

// Carter
builder.Services.AddCarter();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event Echos API",
        Description = "New API Project"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http, // Changed from ApiKey to Http
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and the JWT token."
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
            new List<string>()
        }
    });
});

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Enable logging for better diagnostics
app.Logger.LogInformation("Application Starting...");

// Carter middleware
app.MapCarter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigins");

app.MapControllers();

app.Run();

