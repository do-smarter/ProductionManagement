using Erfa.PruductionManagement.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();

await app.ResetDatabaseAsync();

public partial class Program { }