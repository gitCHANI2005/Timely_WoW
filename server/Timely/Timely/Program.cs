using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mock;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//di -database
builder.Services.AddDbContext<IContext, DataBase>();
builder.Services.AddScoped<IService<CityDto>, CityService>();
builder.Services.AddScoped<IService<CategoryDto>, CategoryService>();
builder.Services.AddScoped<IService<MenuDoseDto>, MenuDoseService>();
builder.Services.AddScoped<IService<CustomerDto>, CustomerService>();
builder.Services.AddScoped<IService<DeliverDto>, DeliverService>();
builder.Services.AddScoped<IService<ExtraDto>, ExtraService>();
builder.Services.AddScoped<IService<OrderDto>, OrderService>();
builder.Services.AddScoped<IService<OwnerDto>, OwnerService>();
builder.Services.AddScoped<IService<StoreDto>, StoreService>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<OwnerService>();
//builder.Services.AddScoped<DeliverService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IRepository<Owner>, OwnerRepository>();
builder.Services.AddScoped<IRepository<Deliver>, DeliverRepository>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();

//builder.Services.AddScoped<IRepository, OwnerRepository>();
//di
builder.Services.AddServiceExtension();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT Bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
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
            new string[] { }
        }
    });
});

// הוספת השירות JwtService ל-DI
builder.Services.AddScoped<JwtService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    option.TokenValidationParameters = new TokenValidationParameters()
    {

        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    }
    );

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "admin"));

    options.AddPolicy("ManagerOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "manager"));

    options.AddPolicy("DeliverOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "deliver"));

    options.AddPolicy("CustomerOnly", policy =>
        policy.RequireClaim(ClaimTypes.Role, "customer"));
});

// enable cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000") // ? ????? ?????? ????
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// הוספת שירותי רישום (לפי הצורך)
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// שימוש במדיניות CORS
app.UseCors(MyAllowSpecificOrigins);

// middleware עבור אימות
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
