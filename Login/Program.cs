using Login.Data;
using Login.Healper;
using Login.Models;
using Login.Repository;
using Login.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//To read the Connection string from the Appsetting.json File  
var connectionstring=builder.Configuration.GetConnectionString("Login");
//Entity Model which Database server using 
builder.Services.AddDbContext<LoginContext>(option =>
{
    option.UseSqlServer(connectionstring);
});
//builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<LoginContext>();
//Identity and Role of EF Context
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<LoginContext>();
//Configure the Password rules
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequiredLength=8;
    
});
//Set LoginPath
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/SignIn";
});

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
//{
//    options.LoginPath = "/SignIn";
//});

builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = "295649815734-900o0bqr8is4ssib44j01k7ktdo72khm.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
//builder.Services.AddAuthentication().AddIdentityCookies();
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimPrincipalFactory>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.Run();
