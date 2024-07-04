using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookStoreMgmt.Data;
using BookStoreMgmt.CustomExceptionMiddleware;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using BookStoreMgmt.Repository.Interface;
using BookStoreMgmt.Repository;
using BookStoreMgmt.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using NLog;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookStoreMgmtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreMgmtContext") ?? throw new InvalidOperationException("Connection string 'BookStoreMgmtContext' not found.")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BookStoreMgmtContext>().AddDefaultTokenProviders(); ;
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";

});
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler(opt => { });
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
