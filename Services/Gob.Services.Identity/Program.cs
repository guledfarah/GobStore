using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Services;
using Gob.Services.Identity.Configs;
using Gob.Services.Identity.DbContexts;
using Gob.Services.Identity.Initializer;
using Gob.Services.Identity.Models;
using Gob.Services.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var conStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("DefaultConnection"))
{
    Password = builder.Configuration["DbPassword"] 
};
var connection = conStrBuilder.ConnectionString;
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

// Register Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure IdentityServer
var identityServerBuilder = builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseSuccessEvents = true;

        // TODO: Reconsider when going online
        options.EmitStaticAudienceClaim = true;
        
    }).AddInMemoryIdentityResources(StaticDetail.IdentityResources)
    .AddInMemoryApiScopes(StaticDetail.ApiScopes)
    .AddInMemoryClients(StaticDetail.Clients)
    .AddAspNetIdentity<ApplicationUser>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

//Add a local developer signing credential
// TODO: Reconsider when going online
identityServerBuilder.AddDeveloperSigningCredential();

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

app.UseIdentityServer();

app.UseAuthorization();

SeedDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}