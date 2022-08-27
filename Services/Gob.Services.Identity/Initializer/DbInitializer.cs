using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gob.Services.Identity.Configs;
using Gob.Services.Identity.DbContexts;
using Gob.Services.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Gob.Services.Identity.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    
    public DbInitializer(ApplicationDbContext applicationDbContext, 
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _db = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public void Initialize()
    {
        // If this is the first run of the app, lets initialize some roles and users
        if (_roleManager.FindByNameAsync(StaticDetail.Admin).Result != null) return;
        _roleManager.CreateAsync(new IdentityRole(StaticDetail.Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(StaticDetail.Customer)).GetAwaiter().GetResult();

        // Admin initial user
        var adminUser = new ApplicationUser()
        {
            FirstName = "Gob",
            LastName = "Admin",
            UserName = "gobadmin",
            Email = "gobadmin@gobstore.com",
            EmailConfirmed = true,
            PhoneNumber = "6141111111"
        };
        _userManager.CreateAsync(adminUser, _configuration["adminUserPassword"]).GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser, StaticDetail.Admin).GetAwaiter().GetResult();
        var adminClaimIdentityResult = _userManager.AddClaimsAsync(adminUser, new List<Claim>()
        {
            new Claim(JwtClaimTypes.Name, $"{adminUser.FirstName} {adminUser.LastName}"),
            new Claim(JwtClaimTypes.GivenName, $"{adminUser.FirstName}"),
            new Claim(JwtClaimTypes.FamilyName, $"{adminUser.LastName}"),
            new Claim(JwtClaimTypes.Role, $"{StaticDetail.Admin}"),
        }).Result;

        // Customer initial user
        var customerUser = new ApplicationUser()
        {
            FirstName = "Johny",
            LastName = "Cust",
            UserName = "johnycust",
            Email = "johnycust@gobstore.com",
            EmailConfirmed = true,
            PhoneNumber = "6142222222"
        };
        _userManager.CreateAsync(customerUser, _configuration["customerUserPassword"]).GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customerUser, StaticDetail.Customer).GetAwaiter().GetResult();
        var customerClaimIdentityResult = _userManager.AddClaimsAsync(customerUser, new List<Claim>()
        {
            new Claim(JwtClaimTypes.Name, $"{customerUser.FirstName} {customerUser.LastName}"),
            new Claim(JwtClaimTypes.GivenName, $"{customerUser.FirstName}"),
            new Claim(JwtClaimTypes.FamilyName, $"{customerUser.LastName}"),
            new Claim(JwtClaimTypes.Role, $"{StaticDetail.Customer}"),
        }).Result;

    }
}