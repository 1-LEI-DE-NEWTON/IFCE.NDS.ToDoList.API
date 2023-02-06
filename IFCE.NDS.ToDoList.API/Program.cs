using IFCE.NDS.ToDoList.API.Configuration;
using NDS_ToDo.Application;
using NDS_ToDo.Infra;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder
    .Services
    .AddInfraData(builder.Configuration.GetConnectionString("DefaultConnection"));

builder
    .Services
    .AddApplication(builder.Configuration);

builder
    .Services
    .AddApiConfiguration();

builder
    .Services
    .AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration(app.Environment);

app.UseApplicationConfiguration();

if (app.Environment.IsProduction())
{
    app.UseMigrations(app.Services);
}

app.Run();
