using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Truextend.Scheduling.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
            builder.Configuration.AddEnvironmentVariables();

            IConfigurationSection LoggerPath = builder.Configuration.GetSection("Logger").GetSection("LogPath");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Information()
                .WriteTo.File(LoggerPath.Value, LogEventLevel.Information)
                .CreateLogger();
            Log.Information($"Json file setup has been read: appsettings.{builder.Environment.EnvironmentName}.json");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                );
            });

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

