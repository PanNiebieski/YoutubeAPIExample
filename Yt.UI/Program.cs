using Yt.ApplicationCore;
using Yt.Infrasctructure.Explode;
using Yt.UI.Models;
using YT.Infrastructure.GoogleApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFinderYouTubeService, GoogleYouTubeService>();
//builder.Services.AddSingleton<IFinderYouTubeService, ExplodeYouTubeService>();

//config
var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");
IConfigurationRoot config = configBuilder.Build();
IConfigurationSection section = config.GetSection("youTubeConfig");
YoutubeOptions youtubeOptions= section.Get<YoutubeOptions>();

builder.Services.AddSingleton<YoutubeOptions, YoutubeOptions>(
    (service) => { return youtubeOptions; });


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
