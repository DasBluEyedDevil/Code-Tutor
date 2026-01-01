---
type: "EXAMPLE"
title: "Cart API Endpoints"
---

The Cart controller exposes endpoints for all cart operations. Notice how it handles both authenticated users and anonymous sessions seamlessly.

```csharp
// WebApi/Controllers/CartController.cs

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopFlow.Application.Cart.Commands;
using ShopFlow.Application.Cart.Queries;
using ShopFlow.WebApi.Extensions;

namespace ShopFlow.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CartController> _logger;

    public CartController(IMediator mediator, ILogger<CartController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var query = new GetCartQuery
        {
            UserId = User.GetUserIdOrNull(),
            SessionId = GetOrCreateSessionId()
        };

        var cart = await _mediator.Send(query, cancellationToken);
        return Ok(cart);
    }

    [HttpPost("items")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItem(
        [FromBody] AddToCartRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddToCartCommand
        {
            UserId = User.GetUserIdOrNull(),
            SessionId = GetOrCreateSessionId(),
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpPut("items/{productId:int}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateItemQuantity(
        int productId,
        [FromBody] UpdateQuantityRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateCartItemCommand
        {
            UserId = User.GetUserIdOrNull(),
            SessionId = GetOrCreateSessionId(),
            ProductId = productId,
            Quantity = request.Quantity
        };

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.Error.Contains("not found")
                ? NotFound(new { error = result.Error })
                : BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    [HttpDelete("items/{productId:int}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveItem(
        int productId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveFromCartCommand
        {
            UserId = User.GetUserIdOrNull(),
            SessionId = GetOrCreateSessionId(),
            ProductId = productId
        };

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
    {
        var command = new ClearCartCommand
        {
            UserId = User.GetUserIdOrNull(),
            SessionId = GetOrCreateSessionId()
        };

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("merge")]
    [Authorize]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> MergeCart(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var sessionId = GetSessionIdFromCookie();

        if (string.IsNullOrEmpty(sessionId))
        {
            // No anonymous cart to merge, just return current cart
            var query = new GetCartQuery { UserId = userId };
            var cart = await _mediator.Send(query, cancellationToken);
            return Ok(cart);
        }

        var command = new MergeCartCommand
        {
            UserId = userId,
            SessionId = sessionId
        };

        var result = await _mediator.Send(command, cancellationToken);
        
        // Clear the session cookie after merge
        Response.Cookies.Delete("CartSessionId");
        
        return Ok(result);
    }

    private string GetOrCreateSessionId()
    {
        var sessionId = GetSessionIdFromCookie();
        
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
            Response.Cookies.Append("CartSessionId", sessionId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                MaxAge = TimeSpan.FromDays(30)
            });
        }

        return sessionId;
    }

    private string? GetSessionIdFromCookie()
    {
        return Request.Cookies["CartSessionId"];
    }
}

// WebApi/Models/CartRequests.cs

namespace ShopFlow.WebApi.Models;

public record AddToCartRequest(int ProductId, int Quantity = 1);
public record UpdateQuantityRequest(int Quantity);
```
