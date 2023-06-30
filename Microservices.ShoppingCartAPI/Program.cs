using AutoMapper;
using Microservices.MessageBus;
using Microservices.ShoppingCartAPI.Data;
using Microservices.ShoppingCartAPI.Extensions;
using Microservices.ShoppingCartAPI.Mappings;
using Microservices.ShoppingCartAPI.Service;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microservices.ShoppingCartAPI.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartDetailsService, CartDetailsService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IMessageBus,MessageBus>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

builder.Services
    .AddHttpClient("Product",
        u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"] ?? string.Empty))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.Services
    .AddHttpClient("Coupon",
        u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"] ?? string.Empty))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Applying the BearerAuth scheme globally to all operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.AddAppAuthentication();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

ApplyMigration();
app.MapControllers();

app.Run();

void ApplyMigration()
{
    using IServiceScope scope = app.Services.CreateScope();

    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}