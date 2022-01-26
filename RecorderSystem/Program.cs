using RecorderSystem.Entities;
using RecorderSystem.Services;
using Microsoft.AspNetCore.Identity;
using RecorderSystem.Stores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Admin");
});

//builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(1028));
builder.WebHost.UseUrls("http://localhost:9874");

builder.Services.AddSingleton<ISessionContextProvider, SessionContextProvider>();
builder.Services.AddIdentityCore<User>(options => { });
builder.Services.AddScoped<IUserStore<User>, UserStore>();
builder.Services.AddAuthentication("cookies")
    .AddCookie("cookies", options => options.LoginPath = "/Login");

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
