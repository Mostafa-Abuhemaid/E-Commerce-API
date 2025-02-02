using E_Commerce.Core.Helper;
using E_Commerce.Core.Identity;
using E_Commerce.Core.Repository;
using E_Commerce.Core.Service;
using E_Commerce.Core.Settings;
using E_Commerce.Service;
using ECommerce.Repository.Data;
using ECommerce.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
		.AddEntityFrameworkStores<AppDBContext>()
        .AddDefaultTokenProviders(); 
builder.Services.AddScoped<IProductService, ProductRepository>();   
builder.Services.AddScoped<ICategoryService, CategoryRepository>();
builder.Services.AddScoped<ICartService, CartRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<IOffersService, OfferRepository>();
 builder.Services.AddScoped<IAccountService, AccountRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IFavoriteService, FavoriteRepository>();
builder.Services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
             .AddJwtBearer(o =>
             {
                 o.RequireHttpsMetadata = false;
                 o.SaveToken = false;
                 o.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),

                 ValidateIssuer = true,
                     ValidIssuer = configuration["JWT:Issuer"],

                     ValidateAudience = true,
                     ValidAudience = configuration["JWT:Audience"],

                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero 
                 };
             });


var app = builder.Build();

        
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
