using Microsoft.AspNetCore.Localization;
using TVT.Business;
using TVT.Data;
using TVT.Web.Services;
using TVT.Web.Settings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.AddSingleton<LocService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.Configure<FileSettings>(
    builder.Configuration.GetSection("FileSettings"));


var app = builder.Build();

var supportedCultures = new[]
{
    "az-AZ",
    "en-US",
    "ru-RU"
};

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("az-AZ")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

localizationOptions.RequestCultureProviders.Insert(
    0,
    new CookieRequestCultureProvider());

app.UseRequestLocalization(localizationOptions);

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
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "product",
    pattern: "Product/{slug}",
    defaults: new { controller = "Product", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

