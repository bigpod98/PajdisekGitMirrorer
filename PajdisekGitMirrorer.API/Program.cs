global using PajdisekGitMirrorer.CommonLib;
global using PajdisekGitMirrorer.API;
global using Npgsql;
global using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

Settings.GetSettingsENV();

Task timeCheckThread = new Task(() => timeChecker.checkTime(60));
timeCheckThread.Start();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});
var app = builder.Build();

app.MapGet("/Repositories", () => APIMethods.Repositories.Get());
app.MapGet("/Repository/{id}", (int id) => APIMethods.Repository.Get(id));
app.MapPost("/Repository", (RepoInfo repoInfo) => APIMethods.Repository.Post(repoInfo));
app.MapPut("Repository/{id}", (int id, RepoInfo repoInfo) => APIMethods.Repository.Put(id, repoInfo));
app.MapDelete("/Repository/{id}", (int id) => APIMethods.Repository.Delete(id));
app.MapGet("/Run/{id}", (int id) => APIMethods.Run.Get(id));
app.MapGet("/Run", () => APIMethods.Run.Get());
app.MapPost("/Report", (string value) => APIMethods.Report.Post(value));

app.Run();
