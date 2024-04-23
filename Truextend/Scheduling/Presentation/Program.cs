using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;

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

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGenNewtonsoftSupport();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = builder.Configuration.GetSection("SchedulingAPIInfo")["Name"],
                    Version = builder.Configuration.GetSection("SchedulingAPIInfo")["Version"],
                    Description = builder.Configuration.GetSection("SchedulingAPIInfo")["Description"],
                    Contact = new OpenApiContact
                    {
                        Name = builder.Configuration.GetSection("SchedulingAPIInfo")["Contact:Name"],
                        Email = builder.Configuration.GetSection("SchedulingAPIInfo")["Contact:Email"]
                    }
                });
            });

            builder.Services.ConfigureSwaggerGen(options =>
            {
                options.EnableAnnotations();

                var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, file);
                options.IncludeXmlComments(filePath);
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseCors("AllowAnyOrigin");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", builder.Configuration.GetSection("SchedulingAPIInfo")["Name"]);
                c.DocExpansion(DocExpansion.None);
            });

            app.MapControllers();

            app.Run();
        }
    }
}

