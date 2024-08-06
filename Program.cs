using AICodeAssistant.Data;
using AICodeAssistant.DbContext;
using AICodeAssistant.Models;
using AICodeAssistant.Options;
using AICodeAssistant.Services;
using AICodeAssistant.Middleware;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace AICodeAssistant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();

            // Configure Antiforgery
            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            // Configure DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure application settings
            builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSetting"));

            // Register services
            builder.Services.AddTransient<OpenAIService>();
            builder.Services.AddTransient<MockCodeCompletionService>();
            builder.Services.AddSingleton<ICodeCompletionServiceFactory, CodeCompletionServiceFactory>();

            // Add HttpClient
            builder.Services.AddHttpClient();

            // Add Razor Pages and Blazor
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            var app = builder.Build();

            // Add exception handling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgeryTokens();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
