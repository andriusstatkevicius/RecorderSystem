using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecorderSystem;
using RecordSystemData;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/AdminPanel");
});

// TODO: Check if names can be extracted from application.json
var identityContextConnString = Environment.GetEnvironmentVariable("RecorderSystemIdentityContext");
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddDbContext<AccessDBContext>(opt => opt.UseSqlServer(identityContextConnString,
    sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { })
    .AddEntityFrameworkStores<AccessDBContext>();

var appContextConnectionString = Environment.GetEnvironmentVariable("RecorderSystemAppContext");
builder.Services.AddDbContext<RecordSystemAppContext>(option => option.UseSqlServer(appContextConnectionString));

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

// Allows to modify the mark up on the Razor Pages and reload on the browser to the changes instantly
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapRazorPages();
app.Run();

