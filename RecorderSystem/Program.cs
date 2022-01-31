using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecordSystemData;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/AdminPanel");
});

var identityContextConnectionString = Environment.GetEnvironmentVariable("RecorderSystemIdentityContext");
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(identityContextConnectionString,
    sql => sql.MigrationsAssembly(migrationAssembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { })
    .AddEntityFrameworkStores<IdentityDbContext>();

var recordSystemAppContext = Environment.GetEnvironmentVariable("RecordSystemAppContext");
builder.Services.AddDbContext<RecordSystemAppContext>(option => option.UseSqlServer(recordSystemAppContext));

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

