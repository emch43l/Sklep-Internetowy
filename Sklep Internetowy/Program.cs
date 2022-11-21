using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Sklep_Internetowy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models.Contexts;

CultureInfo[] cultures = new CultureInfo[]
{
    new CultureInfo("pl-PL"),
    new CultureInfo("en-US")
}; 

var builder = WebApplication.CreateBuilder(args);

var connectionDataString = builder.Configuration.GetConnectionString("DataContextConnection") ?? throw new InvalidOperationException("Connection string 'DataContextConnection' not found.");

builder.Services.
    AddDbContext<DataContext>(
        options => options.UseSqlite(connectionDataString)
        );

builder.Services
    .AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<DataContext>();

//string connection = builder.Configuration.GetConnectionString("Connection") ?? throw new ArgumentException("Connection params not found !");

// Add services to the container.
builder.Services
    .AddScoped<IByteParser, ByteParser>()
    .AddScoped<IFileUploader, FileUploader>()
    .AddScoped<IDirectoryConfigurationReader,DirectoryConfigurationReader>();

//builder.Services
//    .AddDbContext<AppDbContext>()
//    .AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture("pl-PL"),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
