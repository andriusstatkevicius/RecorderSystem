using Microsoft.AspNetCore.Identity;
using RecorderSystem;
using RecorderSystem.Services;
using RecordSystemData.DBContexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/AdminPanel");
});

var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddDbContext<AccessDBContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { })
    .AddEntityFrameworkStores<AccessDBContext>();

builder.Services.AddDbContext<RecordSystemAppContext>();
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login");

// Allows to modify the mark up on the Razor Pages and reload on the browser to the changes instantly
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireRegistrationRole",
         policy => policy.RequireRole("Administrator", "Consultant"));
});

builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
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

