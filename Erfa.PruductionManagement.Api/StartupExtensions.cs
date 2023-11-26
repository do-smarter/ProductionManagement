using Erfa.PruductionManagement.Application;
using Erfa.ProductionManagement.Persistance;
using Erfa.PruductionManagement.Api.Middlewares;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Erfa.PruductionManagement.Api
{
    public static class StartupExtensions
    {
        static string policyName = "policy";
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            builder.Services.AddApiVersioning(
              options =>
              {
                  options.ReportApiVersions = true;
                  options.Conventions.Controller<Controllers.V1.ItemController>().HasApiVersion(new ApiVersion(1, 0));
                  options.Conventions.Controller<Controllers.V2.ItemController>().HasApiVersion(new ApiVersion(2, 0));
                  options.Conventions.Controller<Controllers.V1.ProductionGroupController>().HasApiVersion(new ApiVersion(1, 0));
                  options.Conventions.Controller<Controllers.V2.ProductionGroupController>().HasApiVersion(new ApiVersion(2, 0));
                  options.Conventions.Controller<Controllers.V1.ProductionItemController>().HasApiVersion(new ApiVersion(1, 0));
                  options.Conventions.Controller<Controllers.V2.ProductionItemController>().HasApiVersion(new ApiVersion(2, 0));
              }
            );

            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();

            policyName = !configuration["Cors:policyName"].IsNullOrEmpty() ? configuration["Cors:policyName"] : policyName;
            var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: policyName,
                                  policy =>
                                  {
                                      policy.WithOrigins(origins)
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            builder.Services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                                new HeaderApiVersionReader("x-api-version"),
                                                                new MediaTypeApiVersionReader("x-api-version"));
            });
            // Add ApiExplorer to discover versions
            builder.Services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                app.UseSwagger();
                // app.UseSwaggerUI();

                app.UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseCors(policyName);
            app.MapControllers();

            return app;
        }
    }
}
