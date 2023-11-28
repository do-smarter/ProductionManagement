using Erfa.ProductionManagement.Persistance;
using Erfa.PruductionManagement.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetService<ErfaDbContext>();
Console.WriteLine(context.GetType().Name);
if (context != null)
{
    context.Database.Migrate();
}

app.Run();

public partial class Program { }