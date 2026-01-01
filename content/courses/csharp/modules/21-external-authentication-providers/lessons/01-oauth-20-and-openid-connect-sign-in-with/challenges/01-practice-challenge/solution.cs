using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Auth:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth:Google:ClientSecret"]!;
    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.SaveTokens = true;
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", () => Results.Challenge(
    new AuthenticationProperties { RedirectUri = "/profile" },
    new[] { GoogleDefaults.AuthenticationScheme }
));

app.MapGet("/profile", (HttpContext ctx) => Results.Ok(new
{
    Name = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value,
    Email = ctx.User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value
})).RequireAuthorization();

app.Run();