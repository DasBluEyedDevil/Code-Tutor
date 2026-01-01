using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// TODO: Define ShopFlowRoles static class with Admin, Seller, Customer constants

// TODO: Create AdminController
// - Apply [ApiController] and [Route("api/admin")]
// - Apply [Authorize(Roles = ShopFlowRoles.Admin)] to the entire controller
// - Inject UserManager<ApplicationUser> via constructor
// - Create POST "users/{userId}/promote-to-seller" endpoint
//   - Find user by ID
//   - Check if already seller
//   - Add seller role if not
//   - Return appropriate responses

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
}