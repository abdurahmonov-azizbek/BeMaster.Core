// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

namespace BeMaster.Api.Configurations
{
    public static partial class HostConfiguration
    {
        public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
        {
            builder
                .AddDevTools()
                .AddExposers()
                .AddBrokers()
                .AddServices()
                .ConfigureSettings()
                .AddDbContexts()
                .AddCors()
                .ApplyMigrations();

            return new(builder);
        }

        public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
        {
            app
                .UseDevTools()
                .UseExposers()
                .UseCors();

            app.UseCustomMiddlewares();

            return new(app);
        }
    }
}
