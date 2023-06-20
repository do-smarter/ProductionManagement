using Erfa.PruductionManagement.Application;
using Erfa.ProductionManagement.Persistance;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Erfa.PruductionManagement.Api.Middlewares;

namespace Erfa.PruductionManagement.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {

            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Production Management API");
                });
            }

            app.UseHttpsRedirection();
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseCors("Open");
            app.MapControllers();

            return app;
        }


        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProductionManagement API",

                });
               // c.OperationFilter<FileResultContentTypeOpertationFilter>();
            });
        }


        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<ErfaDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                //add logging here
            }

        }
    }
}
