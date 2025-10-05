
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mini_Library.CustomExceptionMiddelWare;
using Persistence.Data.Configurations;
using Persistence.Identity;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mini_Library
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<LibraryIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<LibraryIdentityDbContext>()
                            .AddDefaultTokenProviders();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserBorrowRepository, UserBorrowRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserBorrowService, UserBorrowService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
            builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
            builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
            builder.Services.AddScoped<IShelfRepository, ShelfRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IShelfService, ShelfService>();
            builder.Services.AddScoped<IAuthService, AuthService>();    
            builder.Services.AddAutoMapper(Config =>
            {
                Config.AddMaps(typeof(AssemplyRefernceMappingProfile).Assembly);
            });
            #endregion

            #region JWT Bearer Authentication Middleware
            builder.Services.AddAuthentication(optins =>
            {
                optins.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                optins.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]!))
                };
            });
                
            #endregion

            var app = builder.Build();
            #region Role Seeding 
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = new[] { "Admin", "User", "Employee" };
            foreach(var role in roles)
            {
                if(! await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(Options =>
                {
                    Options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration = true,
                    };
                    Options.DocumentTitle = "My Library API Documentation";
                    Options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    Options.DocExpansion(DocExpansion.None);
                    Options.EnableFilter();
                    Options.EnablePersistAuthorization();
                });
            }

            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();   
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
