using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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
using Timely.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI - Database Services
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
builder.Services.AddScoped<IRegisterUser<Customer, CustomerDto>, CustomerService>();
builder.Services.AddScoped<IRegisterUser<Deliver, DeliverDto>, DeliverService>();
builder.Services.AddScoped<IRegisterUser<Owner, OwnerDto>, OwnerService>();




// Additional Scoped Services
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IRepository<Owner>, OwnerRepository>();
builder.Services.AddScoped<IRepository<Deliver>, DeliverRepository>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrders<Order>, OrderRepository>();


builder.Services.AddSignalR();

builder.Services.AddServiceExtension();

// Swagger JWT Configuration
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

// JWT Service Configuration
builder.Services.AddScoped<JwtService>();

// JWT Authentication Configuration
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
    });

// Authorization Policies Configuration
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

// CORS Configuration
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Logging Services
builder.Services.AddLogging();

var app = builder.Build();

app.UseCors("MyAllowSpecificOrigins");

// Swagger and Development Environment Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Common Middleware
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

// SignalR - הגדרת נתיב ה-Hub
app.UseWebSockets();
app.MapHub<ChatHub>("/chatHub");  // הוספתי את זה כאן

app.MapControllers();

app.Run();
