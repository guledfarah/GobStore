using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Gob.Services.Identity.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Gob.Services.Identity.Services;

public class ProfileService : IProfileService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProfileService(
        UserManager<ApplicationUser> userMgr,
        RoleManager<IdentityRole> roleMgr,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userMgr;
        _roleManager = roleMgr;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        string subjectId = context.Subject.GetSubjectId();

        ApplicationUser user = await _userManager.FindByIdAsync(subjectId);
        ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

        List<Claim> claims = userClaims.Claims.ToList();
        // Search and Add the requested claims
        claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
        // Apart from the requested claims, Add application custom claims if any
        claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        // Check if the application supports user claims and add them if they exists
        if (_userManager.SupportsUserRole)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                // Check id application supports role claims and add them if they exist
                if (_roleManager.SupportsRoleClaims)
                {
                    IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        claims.AddRange(await _roleManager.GetClaimsAsync(role));
                    }
                }
            }
        }

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string subjectId = context.Subject.GetSubjectId();
        ApplicationUser user = await _userManager.FindByIdAsync(subjectId);
        context.IsActive = user != null;
    }
}