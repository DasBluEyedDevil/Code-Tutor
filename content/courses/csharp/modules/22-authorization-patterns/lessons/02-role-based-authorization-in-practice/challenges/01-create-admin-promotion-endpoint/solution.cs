using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public static class ShopFlowRoles
{
    public const string Admin = "Admin";
    public const string Seller = "Seller";
    public const string Customer = "Customer";
}

[ApiController]
[Route("api/admin")]
[Authorize(Roles = ShopFlowRoles.Admin)]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    
    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpPost("users/{userId}/promote-to-seller")]
    public async Task<IActionResult> PromoteToSeller(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();
        
        if (await _userManager.IsInRoleAsync(user, ShopFlowRoles.Seller))
            return Ok("User is already a seller");
        
        var result = await _userManager.AddToRoleAsync(user, ShopFlowRoles.Seller);
        
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        return Ok("User promoted to seller");
    }
}

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}