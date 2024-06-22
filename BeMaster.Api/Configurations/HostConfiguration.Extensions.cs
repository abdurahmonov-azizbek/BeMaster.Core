// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

using BeMaster.Api.CustomMiddlewares;
using BeMaster.Application.Common.Identity;
using BeMaster.Domain.Common.Settings;
using BeMaster.Infrastructure.Common.Identity;
using BeMaster.Persistence.DbContexts;
using BeMaster.Persistence.Repositories;
using BeMaster.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BeMaster.Api.Configurations
{
    public static partial class HostConfiguration
    {
        // WebApplicationBuilder configurations
        private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();

            return builder;
        }

        private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            return builder;
        }

        private static WebApplicationBuilder AddDbContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return builder;
        }

        private static WebApplicationBuilder AddBrokers(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IUserRepository, UserRepository>();

            return builder;
        }

        private static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<IUserService, UserService>();

            return builder;
        }

        private static WebApplicationBuilder ConfigureSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

            return builder;
        }
        private static WebApplicationBuilder ApplyMigrations(this WebApplicationBuilder builder)
        {
            using var scope = builder.Services.BuildServiceProvider().CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            return builder;
        }

        private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options => options.AddPolicy(name: "AllowSpecificOrigin", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            return builder;
        }

        // WebApplication configurations
        private static WebApplication UseExposers(this WebApplication app)
        {
            app.MapControllers();

            return app;
        }

        private static WebApplication UseDevTools(this WebApplication app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI();

            return app;
        }

        private static WebApplication UseCors(this WebApplication app)
        {
            app.UseCors("AllowSpecificOrigin");

            return app;
        }

        private static WebApplication UseCustomMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }
    }
}
