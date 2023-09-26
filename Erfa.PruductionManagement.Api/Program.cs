using Erfa.ProductionManagement.Persistance;
using Erfa.PruductionManagement.Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();


using var scope = app.Services.CreateScope();

var prodContext = scope.ServiceProvider.GetService<ErfaProductionDbContext>();
if (prodContext != null)
{
    prodContext.Database.Migrate();
}
var archContext = scope.ServiceProvider.GetService<ErfaArchiveDbContext>();
if (archContext != null)
{
    archContext.Database.Migrate();
}


public partial class Program { }