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

builder.Services.AddAuthentication("AdminCookie")
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.AccessDeniedPath = "/Admin/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

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
    new RouteDataRequestCultureProvider());

localizationOptions.RequestCultureProviders.Insert(
    1,
    new CookieRequestCultureProvider());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "category",
    pattern: "{culture:regex(az|en|ru)}/{slug}",
    defaults: new
    {
        controller = "Category",
        action = "Index"
    });

app.MapControllerRoute(
    name: "product",
    pattern: "Product/{slug}",
    defaults: new
    {
        controller = "Product",
        action = "Index"
    });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
