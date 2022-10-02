using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UM.DataAccess.DataContext;
using UM.DataAccess.Repository.Identity;
using UM.REST.Infrastructure.Middleware;
using UM.ServiceProvider;
using UM.ServiceProvider.InternalService.Authentication;
using UM.ServiceProvider.Service.Identity;

namespace UM.REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<EfCoreContext>(op =>
                op.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UM;")
                .EnableSensitiveDataLogging());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("Identity", new OpenApiInfo
                {
                    Title = "Identity Api",
                    Version = "v1"
                });
                op.CustomSchemaIds(x =>
                {
                    return x.ToString();
                });
            });

            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            //builder.Services.AddScoped<IAccountService, AccountService2>();
            //builder.Services.AddScoped<IAccountService, AccountService3>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IAuthUser, AuthUser>();
            builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(op =>
                {
                    op.SwaggerEndpoint("/swagger/Identity/swagger.json", "Identity Api v1");
                });
            }

            app.UseHttpsRedirection();

            // Equal To: app.UseMiddleware<UrlResolverPipeline>();
            app.UseUrlResolverPipeline();

            app.UseApiAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}